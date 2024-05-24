using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Serialization;
using System.Collections;
using System.Linq;
using System.Xml;
using static UnityEngine.JsonUtility;



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
    [Serializable] // Asegúrate de que esta clase sea serializable
    public class DatosPlaca // Nueva clase para representar los datos de una placa
    {
        public string Placa;
        public List<SensorData> Datos; 
    }
    public int _serverPort;
    public string _serverIp = "192.168.1.";
    
    
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
            AlmacenarDatos();
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

            _datosParaEnviar = true; // Indicar que hay datos para enviar
        }
    }
    private bool SonDatosIguales(SensorData datos1, SensorData datos2)
    {
        return datos1.Temperatura == datos2.Temperatura &&
               datos1.Puertas == datos2.Puertas &&
               datos1.Luminosidad == datos2.Luminosidad &&
               datos1.Movimiento == datos2.Movimiento &&
               datos1.Humedad == datos2.Humedad;
    }

    private void AlmacenarDatos()
    {
        // Convertir el diccionario datosPorPlaca a una lista de DatosPlaca
        List<DatosPlaca> datosParaJson = datosPorPlaca.Select(
            placa => new DatosPlaca { Placa = placa.Key, Datos = placa.Value.Values.ToList() }
        ).ToList();

        // Serializar a JSON usando JsonUtility
        string json = JsonUtility.ToJson(new { Placas = datosParaJson }, true);

        // Ruta completa del archivo en el directorio actual
        string rutaArchivo = Path.Combine("/Users/Daniil/Documents/GitHub/TFG/", "Datos.json");

        try
        {
            File.WriteAllText(rutaArchivo, json);
            Debug.Log("Datos guardados en: " + rutaArchivo);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar los datos: " + e.Message);
        }
    }
}