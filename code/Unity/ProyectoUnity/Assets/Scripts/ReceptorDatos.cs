using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Serialization;
using System.Collections;

public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    public int _serverPort;
    public string _serverIp = "192.168.1.";
    
    private bool _lastMov;
    private float _lastTime;
    private Dictionary<string, bool> estadoPuertas = new Dictionary<string, bool>();

    private Dictionary<string, Dictionary<string, object>> datosPlacas =
        new Dictionary<string, Dictionary<string, object>>();
    
    private Dictionary<string, Dictionary<string, object>> _ultimosDatosPlacas =
        new Dictionary<string, Dictionary<string, object>>();
    [FormerlySerializedAs("CicloDn")] public CicloDN cicloDn;

    private bool _datosParaEnviar;

    // Start is called before the first frame update
    void Start()
    {
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
            SendDataToParent();
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
        if (!datosPlacas.ContainsKey(nombrePlaca))
        {
            datosPlacas[nombrePlaca] = new Dictionary<string, object>();
        }
        datosPlacas[nombrePlaca][tipoDato] = valor;
        if (datosPlacas != _ultimosDatosPlacas)
        {
            _ultimosDatosPlacas = datosPlacas;
            _datosParaEnviar = true;
        }
    }

    private void SendDataToParent()
    {
        
    }
}

/*private void SendDataToServer()
{
    try
    {
        string filePath = Path.Combine("C:/Users/Daniil/Documents/GitHub/TFG", "sensor_data.txt");

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if (!pathImpreso)
            {
                //Debug.Log("Path de almacenamiento de los datos: " + filePath);
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
                if (mensaje.Contains("PlacaRecibidor"))
                {
                    Debug.Log(mensaje);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Debug.LogError("Error al enviar datos al servidor: " + ex.Message);
    }
}
[Serializable]
public class SensorData
{
    public float Temperatura;
    public string Puertas;
    public float Luminosidad;
    public bool Movimiento;
    public float Sonido;
    public float Presion;
    public float Humedad;
}

[Serializable]
public class PlacaData
{
    public string Nombre;
    public List<SensorData> Datos = new List<SensorData>();
}

[Serializable]
public class RegistroDatos
{
    public string FechaHora;
    public List<PlacaData> Placas = new List<PlacaData>();
}
/*private void SaveDataToJson()
{
    try
    {
        string filePath = Path.Combine("C:/Users/dgall/Desktop/TFG", "sensor_data.json");

        // Crear una instancia de RegistroDatos
        RegistroDatos registro = new RegistroDatos
        {
            FechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // Iterar sobre las placas y sus datos
        foreach (var placa in datosPlacas)
        {
            PlacaData placaData = new PlacaData
            {
                Nombre = placa.Key
            };

            foreach (var dato in placa.Value)
            {
                SensorData sensorData = new SensorData
                {
                    Temperatura = (float)dato.Value["Temperatura"],
                    Puertas = (string)dato.Value["Puertas"],
                    Luminosidad = (float)dato.Value["Luminosidad"],
                    Movimiento = (bool)dato.Value["Movimiento"],
                    Sonido = (float)dato.Value["Sonido"],
                    Presion = (float)dato.Value["Presion"],
                    Humedad = (float)dato.Value["Humedad"]
                };
                placaData.Datos.Add(sensorData);
            }

            registro.Placas.Add(placaData);
        }

        // Serializar a JSON y guardar en el archivo
        string jsonString = JsonUtility.ToJson(registro, true); // El segundo parámetro 'true' formatea el JSON para mejor legibilidad
        File.WriteAllText(filePath, jsonString);

        if (!pathImpreso)
        {
            Debug.Log("Path de almacenamiento de los datos: " + filePath);
            pathImpreso = true;
        }
    }
    catch (Exception ex)
    {
        Debug.LogError("Error al guardar datos en el archivo JSON: " + ex.Message);
    }
}
}*/
