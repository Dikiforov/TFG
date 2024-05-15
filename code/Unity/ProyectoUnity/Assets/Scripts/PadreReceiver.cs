using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Serialization;

public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
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
            SendDataToServer();
        }
    }

    public void RecieveTempData(float temperature, bool enviarData)
    {
        //Debug.Log("Temperatura: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastTemperature = temperature;
        }
        //Debug.Log("_datosParaEnviar desde Temp: "+_datosParaEnviar);
    }

    public void RecieveDoorState(bool isOpen, string doorName)
    {
        if (estadoPuertas.ContainsKey(doorName))
        {
            // Si el nombre de la puerta no esta en el diccionario de la placa, se añade
            if (estadoPuertas[doorName] != isOpen)
            {
                estadoPuertas[doorName] = isOpen;
                _datosParaEnviar = true;
            }
        }
        else
        {
            estadoPuertas.Add(doorName, isOpen);
        }
        //Debug.Log("_datosParaEnviar desde Puertas en "+doorName+": "+_datosParaEnviar);
    }

    public void RecieveHumedadData(float humedad, bool enviarData)
    {
        //Debug.Log("Humedad: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastHumedad = humedad;
        }
        //Debug.Log("_datosParaEnviar desde Humedad: "+_datosParaEnviar);
    }

    public void RecieveLuminosidadData(float luminosidad, bool enviarData)
    {
        //Debug.Log("Luminosidad: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastLum = luminosidad;
        }
        Debug.Log("_datosParaEnviar desde Luminosidad: "+ luminosidad);
    }

    public void RecieveMovimientoData(bool movement)
    {
        //Debug.Log("Movimiento: " + movement);
        if (_lastMov != movement)
        {
            _datosParaEnviar = true;
            _lastMov = movement;
        }
        //Debug.Log("_datosParaEnviar desde Movimiento: "+_datosParaEnviar);
    }

    public void RecieveSonidoData(float sound, bool enviarData)
    {
        //Debug.Log("Sonido: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastSonido = sound;
        }
        //Debug.Log("_datosParaEnviar desde Sonido: "+_datosParaEnviar);
    }

    public void RecievePresionData(float presion, bool enviarData)
    {
        //Debug.Log("Presión: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastPresion = presion;
        }
        //Debug.Log("_datosParaEnviar desde Presión: "+_datosParaEnviar);
    }

    private void SendDataToServer()
    {
        try
        {
            // Crear mensaje con los datos a enviar al servidor
            StringBuilder messageBuilder = new StringBuilder();
            TimeSpan tiempo = CicloDN.horaFormateada;
            messageBuilder.Append(this.gameObject.name + " " + string.Format("{0:D2}:{1:D2}:{2:D2}", tiempo.Hours, tiempo.Minutes, tiempo.Seconds) + "" + ";");
            messageBuilder.Append("Temperatura:" + (_lastTemperature.ToString("0.0")).Replace(',', '.') + ",");
            string auxDoorStates = "";
            foreach (var kDoor in estadoPuertas)
            {
                auxDoorStates += kDoor.Key + '=' + kDoor.Value + ' ';
            }
            messageBuilder.Append("Puertas:" + auxDoorStates.TrimEnd() + ',');
            messageBuilder.Append("Luminosidad:" + _lastLum + ",");
            messageBuilder.Append("Movimiento:" + _lastMov + ",");
            messageBuilder.Append("Sonido:" + _lastSonido + ",");
            messageBuilder.Append("Presion:" + _lastPresion + ",");
            messageBuilder.Append("Humedad:" + _lastHumedad);
        
            string message = messageBuilder.ToString();
            using (TcpClient client = new TcpClient(_serverIp, _serverPort))
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //Debug.Log("Envio el mensaje: " + message);
                // Enviar el mensaje al servidor
                writer.WriteLine(message);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al enviar datos al servidor: " + ex.Message);
        }
    }
}
