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
    
    private bool _cambiosTemp;
    private bool _cambiosDoor;
    private bool _cambiosHumedad;
    private bool _cambiosLum;
    private bool _cambiosMovimiento;
    private bool _cambiosSonido;
    private bool _cambiosPresion;
    
    private string _nombrePuerta = "";
    
    [FormerlySerializedAs("CicloDn")] public CicloDN cicloDn;

    private const float Tolerance = 5; 
    // Start is called before the first frame update
    void Start()
    {
        cicloDn = FindObjectOfType<CicloDN>();
    }

    private void Update()
    {
        var hora = CicloDN.Hora;
        if ((hora - _lastTime) > 300 || _cambiosTemp || _cambiosDoor)
        {
            _lastTime = hora;
            SendDataToServer();
        }
    }

    public void RecieveTempData(float temperature, bool enviarData)
    {
        // Comparar el nuevo valor con el anterior
        if (Math.Abs(temperature - _lastTemperature) > Tolerance || enviarData)
        {
            _lastTemperature = temperature;
            _cambiosTemp = true;
        }
        
    }

    public void RecieveDoorState(bool isOpen, string doorName)
    {
        // Comparar el nuevo valor con el anterior
        if (isOpen != _lastDoorState)
        {
            // Actualizar el último valor conocido
            _lastDoorState = isOpen;
            _cambiosDoor = true;
            _nombrePuerta = doorName;
        }
    }

    public void RecieveHumedadData(float humedad, bool enviarData)
    {
        if (humedad != _lastHumedad || enviarData)
        {
            _lastHumedad = humedad;
            _cambiosHumedad = true;
        }
    }

    public void RecieveLuminosidadData(float luminosidad, bool enviarData)
    {
        if (luminosidad != _lastLum || enviarData)
        {
            _lastLum = luminosidad;
            _cambiosLum = true;
        }
    }

    public void RecieveMovimientoData(bool movement, bool enviarData)
    {
        if (movement != _lastMov || enviarData)
        {
            _lastMov = movement;
            _cambiosMovimiento = true;
        }
    }

    public void RecieveSonidoData(float sound, bool enviarData)
    {
        if (sound != _lastSonido || enviarData)
        {
            _lastSonido = sound;
            _cambiosSonido = true;
        }
    }

    public void RecievePresionData(float presion, bool enviarData)
    {
        if (presion != _lastPresion || enviarData)
        {
            _lastPresion = presion;
            _cambiosPresion = true;
        }
    }

    private void SendDataToServer()
    {
        try
        {
            // Crear mensaje con los datos a enviar al servidor
            StringBuilder messageBuilder = new StringBuilder();
            TimeSpan tiempo = CicloDN.horaFormateada;
            messageBuilder.Append(this.gameObject.name +"(" +string.Format("{0:D2}:{1:D2}:{2:D2}", tiempo.Hours, tiempo.Minutes, tiempo.Seconds) + ")"+";");
            if (_cambiosTemp)
            {
                _cambiosTemp = false;
                messageBuilder.Append("Temperatura:" + _lastTemperature + ",");
            }

            if (_cambiosDoor)
            {
                _cambiosDoor = false;
                messageBuilder.Append("Puertas:"+_nombrePuerta + " " + _lastDoorState + ",");
            }
            
            if(_cambiosLum)
            {
                _cambiosLum = false;
                messageBuilder.Append("Luminosidad:" + _lastLum + ",");
            }
            if(_cambiosMovimiento)
            {
                _cambiosMovimiento = false;
                messageBuilder.Append("Movimiento:" + _lastMov + ",");
            }
            if(_cambiosSonido)
            {
                _cambiosSonido = false;
                messageBuilder.Append("Sonido:" + _lastLum + ",");
            }
            if(_cambiosPresion)
            {
                _cambiosPresion = false;
                messageBuilder.Append("Presion:" + _lastPresion + ",");
            }
            if(_cambiosHumedad)
            {
                _cambiosHumedad = false;
                messageBuilder.Append("Humedad:" + _lastHumedad);
            }
    
            string message = messageBuilder.ToString();
            using (TcpClient client = new TcpClient("127.0.0.1", 8080))
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
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
