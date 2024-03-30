using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class PadreReceiver : MonoBehaviour, ISensorDataReciever
{
    private float lastTemperature = float.NaN;
    private bool lastDoorState = false;
    private float lastTime = 0f;
    private bool cambiosTemp = false;
    private bool cambiosDoor = false;    
    
    public CicloDN CicloDn;
    
    // Start is called before the first frame update
    void Start()
    {
        CicloDn = FindObjectOfType<CicloDN>();
    }

    private void Update()
    {
        var hora = CicloDN.TempActual;
        if ((hora - lastTime) > 300 || cambiosTemp || cambiosDoor)
        {
            lastTime = hora;
            SendDataToServer();
        }
    }

    public void RecieveTempData(float temperature)
    {
        // Comparar el nuevo valor con el anterior
        if (temperature != lastTemperature)
        {
            lastTemperature = temperature;
            cambiosTemp = true;
        }
        
    }

    public void RecieveDoorState(bool isOpen)
    {
        // Comparar el nuevo valor con el anterior
        if (isOpen != lastDoorState)
        {
            // Actualizar el último valor conocido
            lastDoorState = isOpen;
            cambiosDoor = true;
        }
    }

    private void SendDataToServer()
    {
        Debug.Log("Procediendo a enviar datos");
        try
        {
            // Crear mensaje con los datos a enviar al servidor
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.Append(this.gameObject.name + "\n");
            if (cambiosTemp)
            {
                cambiosTemp = false;
                messageBuilder.Append("Temperatura:");
                messageBuilder.Append(lastTemperature+" "+lastTime);
                Debug.Log("Mensaje de temperatura creado");
            }

            if (cambiosDoor)
            {
                cambiosDoor = false;
                if (messageBuilder.Length > 0) messageBuilder.Append(";");
                messageBuilder.Append("Puerta:");
                messageBuilder.Append(lastDoorState);
            }
    
            string message = messageBuilder.ToString();
            Debug.Log("Creado string");
            using (TcpClient client = new TcpClient("127.0.0.1", 8080))
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Enviar el mensaje al servidor
                Debug.Log("Enviando mensaje al servidor " + message);
                writer.WriteLine(message);
                Debug.Log("Mensaje enviado al servido");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al enviar datos al servidor: " + ex.Message);
        }
        
    }
}
