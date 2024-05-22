using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Serialization;

public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    private bool pathImpreso;
    public int _serverPort = 1234;
    public string _serverIp = "192.168.1.47";
    private float _lastPresion;
    private float _lastHumedad;
    private float _lastTemperature = float.NaN;
    private bool _lastDoorState;
    private bool _lastMov;
    private float _lastLum;
    private float _lastTime;
    private float _lastSonido;
    private Dictionary<string, bool> estadoPuertas = new Dictionary<string, bool>();
    private Dictionary<string, Dictionary<string, object>> datosPlacas = new Dictionary<string, Dictionary<string, object>>();

    [FormerlySerializedAs("CicloDn")] public CicloDN cicloDn;

    private bool _datosParaEnviar;
    // Start is called before the first frame update
    void Start()
    {
        pathImpreso = false;
        _lastMov = false;
        cicloDn = FindObjectOfType<CicloDN>();
        _lastTime = CicloDN.Hora;
    }

    private void Update()
    {
        var hora = CicloDN.Hora;
        var diferencia = Mathf.Abs(hora - _lastTime);
        
        if (_datosParaEnviar)
        {
            _datosParaEnviar = false;
            _lastTime = hora;
            SendDataToServer();
        }
    }

    public void RecieveTempData(float temperature, bool enviarData, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Temperatura", temperature, enviarData);
    }

    public void RecieveDoorState(bool isOpen, string doorName, string nombrePlaca)
    {
        ActualizarDatoPlaca(nombrePlaca, "Puertas", doorName + "=" + isOpen, true); // Siempre enviar cambios de estado de puertas
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
        if (!datosPlacas.ContainsKey(nombrePlaca))
        {
            datosPlacas[nombrePlaca] = new Dictionary<string, object>();
        }

        datosPlacas[nombrePlaca][tipoDato] = valor;

        if (enviarData)
        {
            _datosParaEnviar = true;
        }
    }
    
    private void SendDataToServer()
    {
        try
        {
            string filePath = Path.Combine("C:/Users/dgall/Desktop/TFG", "sensor_data.txt");

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (!pathImpreso)
                {
                    Debug.Log("Path de almacenamiento de los datos: " + filePath);
                    pathImpreso = true;
                }

                foreach (var placa in datosPlacas)
                {
                    TimeSpan tiempo = CicloDN.horaFormateada;
                    string mensaje = $"{placa.Key} {string.Format("{0:D2}:{1:D2}:{2:D2}", tiempo.Hours, tiempo.Minutes, tiempo.Seconds)};";
                    foreach (var dato in placa.Value)
                    {
                        mensaje += $"{dato.Key}:{dato.Value},";
                    }
                    mensaje = mensaje.TrimEnd(','); // Eliminar la última coma

                    writer.WriteLine(mensaje);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al enviar datos al servidor: " + ex.Message);
        }
    }
}
