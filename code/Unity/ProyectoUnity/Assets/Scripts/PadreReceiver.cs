using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Serialization;

public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    private float _lastPresion;
    private float _lastHumedad;
    private float _lastTemperature = float.NaN;
    private bool _lastDoorState;
    private bool _lastMov;
    private float _lastLum;
    private float _lastTime;
    private float _lastSonido;

    private string _nombrePuerta = "";

    [FormerlySerializedAs("CicloDn")] public CicloDN cicloDn;

    private bool _datosParaEnviar;
    // Start is called before the first frame update
    void Start()
    {
        _lastMov = false;
        cicloDn = FindObjectOfType<CicloDN>();
    }

    private void Update()
    {
        var hora = CicloDN.Hora;
        //Debug.Log("Hora: " + hora + " - " + _lastTime + " Diferencia " + (hora));
        if ((hora - _lastTime) > 300 || _datosParaEnviar)
        {
            _datosParaEnviar = false;
            _lastTime = hora;
            SendDataToServer();
        }
    }

    public void RecieveTempData(float temperature, bool enviarData)
    {
        Debug.Log("Temperatura: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastTemperature = temperature;
        }
    }

    public void RecieveDoorState(bool isOpen, string doorName)
    {
        Debug.Log("Puerta: " + isOpen);
        if (isOpen != _lastDoorState)
        {
            _datosParaEnviar = true;
            _lastDoorState = isOpen;
            _nombrePuerta = doorName;
        }
    }

    public void RecieveHumedadData(float humedad, bool enviarData)
    {
        Debug.Log("Humedad: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastHumedad = humedad;
        }
    }

    public void RecieveLuminosidadData(float luminosidad, bool enviarData)
    {
        Debug.Log("Luminosidad: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastLum = luminosidad;
        }
    }

    public void RecieveMovimientoData(bool movement)
    {
        Debug.Log("Movimiento: " + movement);
        if (_lastMov != movement)
        {
            _datosParaEnviar = true;
            _lastMov = movement;
        }
    }

    public void RecieveSonidoData(float sound, bool enviarData)
    {
        Debug.Log("Sonido: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastSonido = sound;
        }
    }

    public void RecievePresionData(float presion, bool enviarData)
    {
        Debug.Log("Presión: " + enviarData);
        if (enviarData)
        {
            _datosParaEnviar = true;
            _lastPresion = presion;
        }
    }

    private void SendDataToServer()
    {
        try
        {
            // Crear mensaje con los datos a enviar al servidor
            StringBuilder messageBuilder = new StringBuilder();
            TimeSpan tiempo = CicloDN.horaFormateada;
            messageBuilder.Append(this.gameObject.name +" " +string.Format("{0:D2}:{1:D2}:{2:D2}", tiempo.Hours, tiempo.Minutes, tiempo.Seconds) + ""+";");
            messageBuilder.Append("Temperatura:" + _lastTemperature + ",");
            messageBuilder.Append("Puertas:"+_nombrePuerta + " " + _lastDoorState + ",");
            messageBuilder.Append("Luminosidad:" + _lastLum + ",");
            messageBuilder.Append("Movimiento:" + _lastMov + ",");
            messageBuilder.Append("Sonido:" + _lastSonido + ",");
            messageBuilder.Append("Presion:" + _lastPresion + ",");
            messageBuilder.Append("Humedad:" + _lastHumedad);

            string message = messageBuilder.ToString();
            using (TcpClient client = new TcpClient("127.0.0.1", 8080))
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
