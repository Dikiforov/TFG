using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.NetworkInformation; // Agregar para usar Ping
using System.Text;
using UnityEngine.Serialization;
using System.Linq;
using System.IO;
using Object = UnityEngine.Object;
using Ping = System.Net.NetworkInformation.Ping; // Agregar para escribir en archivos



public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    [Serializable]
    public class SensorData
    {
        public string hora;
        public float Temperatura;
        public string Puertas;
        public float Luminosidad;
        public bool Movimiento;
        public float Humedad;
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

    private void Update()
    {
        var hora = CicloDN.Hora;

        if (_datosParaEnviar)
        {
            _datosParaEnviar = false;
            GuardarDatosEnArchivo(datosPorPlaca);
            /*if (PingHost(_serverIp))
            {
                EnviarDatosAlServidor(datosPorPlaca);
            }
            else
            {
                GuardarDatosEnArchivo(datosPorPlaca);
            }*/
        }
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
        bool datosCambiados = ultimosDatos == null ||
                              !SonDatosIguales(ultimosDatos, datosActuales);
        
        // Comprobar si ha pasado el tiempo mínimo desde el último envío
        //bool tiempoTranscurrido = (Time.time - _lastTime) >= intervaloEnvioMinimo;

        if (datosCambiados)
        {
            ultimosDatos = datosActuales; // Actualizar ultimosDatos
            // Almacenar los datos por placa y hora
            if (!datosPorPlaca.ContainsKey(nombrePlaca))
            {
                datosPorPlaca[nombrePlaca] = new Dictionary<string, SensorData>();
            }
            datosPorPlaca[nombrePlaca][datosActuales.hora] = datosActuales;
            /*
            Debug.Log("datosPorPlaca.hora "+datosPorPlaca[nombrePlaca][datosActuales.hora].hora);
            Debug.Log("datosPorPlaca.temperatura "+datosPorPlaca[nombrePlaca][datosActuales.hora].Temperatura);
            Debug.Log("datosPorPlaca.puertas "+datosPorPlaca[nombrePlaca][datosActuales.hora].Puertas);
            Debug.Log("datosPorPlaca.luminosidad "+datosPorPlaca[nombrePlaca][datosActuales.hora].Luminosidad);
            Debug.Log("datosPorPlaca.movimiento "+datosPorPlaca[nombrePlaca][datosActuales.hora].Movimiento);
            Debug.Log("datosPorPlaca.humedad "+datosPorPlaca[nombrePlaca][datosActuales.hora].Humedad);
            */
            _datosParaEnviar = true; // Indicar que hay datos para enviar
        }
    }
    private bool SonDatosIguales(SensorData datos1, SensorData datos2)
    {
        Debug.Log("UltimosDatos.temperatura="+datos1.Temperatura+"--datosActuales.temperatura="+datos2.Temperatura);
        Debug.Log("UltimosDatos.Puertas="+datos1.Puertas+"--datosActuales.Puertas="+datos2.Puertas);
        Debug.Log("UltimosDatos.Luminosidad="+datos1.Luminosidad+"--datosActuales.Luminosidad="+datos2.Luminosidad);
        Debug.Log("UltimosDatos.Movimiento="+datos1.Movimiento+"--datosActuales.Movimiento="+datos2.Movimiento);
        Debug.Log("UltimosDatos.Humedad="+datos1.Humedad+"--datosActuales.Humedad="+datos2.Humedad);
        
        return datos1.Temperatura == datos2.Temperatura &&
               datos1.Puertas == datos2.Puertas &&
               datos1.Luminosidad == datos2.Luminosidad &&
               datos1.Movimiento == datos2.Movimiento &&
               datos1.Humedad == datos2.Humedad;
    }
    private void EnviarDatosAlServidor(Dictionary<string, Dictionary<string, SensorData>> datosPorPlaca)
    {
        // Construir el mensaje en formato "habitacion hora;sensor1:valor1,sensor2:valor2,..."
        StringBuilder message = new StringBuilder();
        foreach (var placa in datosPorPlaca)
        {
            string nombrePlaca = placa.Key;
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

        // Enviar el mensaje al servidor
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

            // Guardar datos en archivo si hay un error de conexión
            GuardarDatosEnArchivo(datosPorPlaca);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al enviar datos al servidor: " + e.Message);
            GuardarDatosEnArchivo(datosPorPlaca);
        }
        
    }
    private bool PingHost(string ipAddress)
    {
        Ping ping = new Ping();
        PingReply reply = ping.Send(ipAddress);
        return reply.Status == IPStatus.Success;
    }
    
    private void GuardarDatosEnArchivo(Dictionary<string, Dictionary<string, SensorData>> datos)
    {
        string filePath = Path.Combine("/users/Daniil/Documents/GitHub/TFG/", "datos_sensor.txt");
        using (StreamWriter writer = new StreamWriter(filePath, true)) // Append to file
        {
            foreach (var placa in datos)
            {
                foreach (var dato in placa.Value)
                {
                    writer.WriteLine(dato.Value.ToString()); // Or your custom format
                }
            }
        }
        Debug.Log("Datos guardados en archivo: " + filePath);
    }
}