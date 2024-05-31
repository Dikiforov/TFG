using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.NetworkInformation; // Agregar para usar Ping
using System.Text;
using UnityEngine.Serialization;
using System.Linq;
using System.IO;
using Unity.VisualScripting;
using Object = UnityEngine.Object;
using Ping = System.Net.NetworkInformation.Ping; // Agregar para escribir en archivos


public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    [Serializable]
    public class SensorData: ICloneable
    {
        public string hora;
        public float Temperatura;
        public string Puertas;
        public float Luminosidad;
        public bool Movimiento;
        public float Humedad;
        public object Clone()
        {
            return new SensorData
            {
                hora = this.hora,
                Temperatura = this.Temperatura,
                Puertas = this.Puertas,
                Luminosidad = this.Luminosidad,
                Movimiento = this.Movimiento,
                Humedad = this.Humedad
            };
        }
    }
    public int _serverPort = 5555;
    public string _serverIp = "192.168.1.41";
    
    
    [FormerlySerializedAs("CicloDn")] public CicloDN cicloDn;

    private bool _datosParaEnviar;

    private SensorData datosActuales = new SensorData();
    private SensorData ultimosDatos;
    private Dictionary<string, Dictionary<string, SensorData>> datosPorPlaca = new Dictionary<string, Dictionary<string, SensorData>>();

    
    // Start is called before the first frame update
    void Start()
    {
        cicloDn = FindObjectOfType<CicloDN>();
    }

    public void RecieveTempData(float temperature, bool enviarData, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Temperatura", temperature, enviarData);
    }

    public void RecieveDoorState(bool isOpen, string doorName, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Puertas", doorName + "=" + isOpen,
            true); // Siempre enviar cambios de estado de puertas
    }

    public void RecieveHumedadData(float humedad, bool enviarData, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Humedad", humedad, enviarData);
    }

    public void RecieveLuminosidadData(float luminosidad, bool enviarData, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Luminosidad", luminosidad, enviarData);
    }

    public void RecieveMovimientoData(bool movement, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Movimiento", movement, true); // Siempre enviar cambios de movimiento
    }

    private void ActualizarDatoPlaca(string nombrePlaca, string tipoDato, object valor, bool enviarData)
    {
        datosActuales.hora = CicloDN.horaFormateada.ToString(@"hh\:mm\:ss");
        switch (tipoDato)
        {
            case "Temperatura":
                datosActuales.Temperatura = (float)valor;
                break;
            case "Puertas":
                datosActuales.Puertas = (string)valor;
                break;
            case "Luminosidad":
                datosActuales.Luminosidad = (float)valor;
                break;
            case "Movimiento":
                datosActuales.Movimiento = (bool)valor; 
                break;
            case "Humedad":
                datosActuales.Humedad = (float)valor; 
                break;
        }
        if (ultimosDatos == null)
        {
            ultimosDatos = (SensorData)datosActuales.Clone();
            GuardarDatosActualesEnArchivo(nombrePlaca);
        }
        else
        {
            TimeSpan diferenciaTiempo = TimeSpan.Parse(datosActuales.hora) - TimeSpan.Parse(ultimosDatos.hora);
            bool tiempoSuficiente = diferenciaTiempo.TotalSeconds >= 300;
            
            if (tiempoSuficiente || !SonDatosIguales(ultimosDatos, datosActuales))
            {
                ultimosDatos = (SensorData)datosActuales.Clone();
                GuardarDatosActualesEnArchivo(nombrePlaca);
            }
        }
    }
    private bool SonDatosIguales(SensorData datos1, SensorData datos2)
    {
        return datos1.Temperatura == datos2.Temperatura &&
               datos1.Luminosidad == datos2.Luminosidad &&
               datos1.Movimiento == datos2.Movimiento &&
               datos1.Humedad == datos2.Humedad;
    }
    private void EnviarDatosAlServidor(Dictionary<string, Dictionary<string, SensorData>> datosPorPlaca)
    {
        // Construir el mensaje en formato "habitacion hora;sensor1:valor1,sensor2:valor2,..."
        StringBuilder message = new StringBuilder();
        string nombrePlaca = "";
        foreach (var placa in datosPorPlaca)
        {
            nombrePlaca = placa.Key;
            var datosPlaca = placa.Value;
            foreach (var dato in datosPlaca)
            {
                string hora = dato.Key;
                SensorData sensorData = dato.Value;

                message.Append($"{nombrePlaca} {hora};");
                message.Append($"Temperatura:{sensorData.Temperatura},");
                message.Append($"Puertas:{sensorData.Puertas},");
                message.Append($"Luminosidad:{sensorData.Luminosidad},");
                message.Append($"Movimiento:{sensorData.Movimiento.ToString().ToLower()},");
                message.Append($"Humedad:{sensorData.Humedad},");
                message.Remove(message.Length - 1, 1); // Eliminar la última coma
                message.Append("\n");
            }
        }
        
        try
        {
            using (TcpClient client = new TcpClient(_serverIp, _serverPort))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(message.ToString());
                stream.Write(data, 0, data.Length);
                Debug.Log("Datos enviados al servidor.");
            }
        }
        catch (SocketException socketEx)
        {
            if (socketEx.SocketErrorCode == SocketError.ConnectionRefused)
            {
                Debug.LogError("Error: El servidor rechazó la conexión. Verifica la configuración del servidor y del firewall.");
            }
            else if (socketEx.SocketErrorCode == SocketError.TimedOut)
            {
                Debug.LogError("Error: Tiempo de espera agotado al intentar conectarse al servidor. Verifica la conectividad de red.");
            }
            else
            {
                Debug.LogError("Error de conexión: " + socketEx.Message);
            }
            GuardarDatosActualesEnArchivo(nombrePlaca);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al enviar datos al servidor: " + e.Message);
            GuardarDatosActualesEnArchivo(nombrePlaca);
        }
        
    }
    private bool PingHost(string ipAddress)
    {
        Ping ping = new Ping();
        PingReply reply = ping.Send(ipAddress);
        return reply.Status == IPStatus.Success;
    }
    
    private void GuardarDatosActualesEnArchivo(string nombrePlaca)
    {
        string filePath = Path.Combine("/users/Daniil/Documents/GitHub/TFG/", "datos_sensor.txt");

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.Write($"{nombrePlaca}[{ultimosDatos.hora}]:");
            writer.Write($"Temperatura={ultimosDatos.Temperatura};");
            writer.Write($"Puertas={ultimosDatos.Puertas};");
            writer.Write($"Luminosidad={ultimosDatos.Luminosidad};");
            writer.Write($"Movimiento={ultimosDatos.Movimiento.ToString().ToLower()};");
            writer.Write($"Humedad={ultimosDatos.Humedad}");
            writer.WriteLine();
        }
    }
}