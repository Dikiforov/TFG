using UnityEngine;
using TMPro;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class DetectionSensor : MonoBehaviour
{
    private TMP_Text movementText;
    private TMP_Text placeText;
    private PlayerMovementTracker playerMovementTracker;
    private bool playerInside = false;
    private bool alreadyDetected = false;

    private void Start()
    {
        movementText = GameObject.Find("TextMovementSensor").GetComponent<TMP_Text>();
        if (movementText == null) Debug.LogError("No se encontró el componente TMP_Text en TextMovementSensor");

        placeText = GameObject.Find("TextPlaceSensor").GetComponent<TMP_Text>();
        if (placeText == null) Debug.LogError("No se encontró el componente TMP_Text en TextPlaceSensor");

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
                ResetText();
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
            ResetText();
        }
    }

    private void ResetText()
    {
        movementText.text = "Movimiento: ";
        placeText.text = "Habitación: ";
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
                movementText.text = "Movimiento: Sí";
                placeText.text = "Habitación: " + this.gameObject.name;
                Debug.Log(movementText.text + " " + placeText.text);
                StartCoroutine(CheckPlayerMovement());
            }
        }
    }
}
