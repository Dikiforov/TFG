using UnityEngine;
using TMPro;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class DetectionSensor : MonoBehaviour
{
    private PlayerMovementTracker playerMovementTracker;
    private bool playerInside = false;
    private bool alreadyDetected = false;

    private void Start()
    {
        playerMovementTracker = FindObjectOfType<PlayerMovementTracker>();
        if (playerMovementTracker == null) Debug.LogError("No se encontró el componente PlayerMovementTracker");
    }



    private IEnumerator CheckPlayerMovement()
    {
        while (playerInside)
        {
            yield return new WaitForSeconds(1f);
            if (!playerMovementTracker.IsMoving)
            {
                alreadyDetected = false;
                playerInside = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            alreadyDetected = false;
        }
    }
    

    private void SendData(string message)
    {
        string formattedMessage = "Movimiento:" + message;  // Agrega la clave "Puertas" antes del mensaje
        //Debug.LogError(formattedMessage);  // Imprime el mensaje formateado
        TcpClient client = new TcpClient("127.0.0.1", 8052);
        byte[] data = System.Text.Encoding.ASCII.GetBytes(formattedMessage);  // Usa el mensaje formateado
        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        client.Close();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovementTracker.IsMoving && !alreadyDetected)
            {
                playerInside = true;
                alreadyDetected = true;
                Debug.Log("Movimiento en la habitación: " + this.gameObject.name);
                StartCoroutine(CheckPlayerMovement());
            }
        }
    }
}
