using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class DoorController : MonoBehaviour
{
    public Transform doorPivot;  // La visagra o punto de rotación de la puerta
    private bool isDoorOpen = false;
    private bool isPlayerNearby = false;
    private float openAngle = -90f;
    private float closedAngle = 0f;
    private float animationTime = 2f;  // Duración de la animación en segundos
    public TMP_Text interactionPrompt;  // Referencia al objeto Text

    private void Start()
    {
        if (doorPivot == null)
        {
            Debug.LogError("No se ha asignado una visagra a la puerta.");
        }
        if (interactionPrompt == null)
        {
            Debug.LogError("No se ha asignado un objeto Text a interactionPrompt.");
        }
        interactionPrompt.gameObject.SetActive(false);  // Oculta el texto al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionPrompt.gameObject.SetActive(true);  // Muestra el texto
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionPrompt.gameObject.SetActive(false);  // Oculta el texto
        }
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(ToggleDoorState());
            }
        }
    }

    private IEnumerator ToggleDoorState()
    {

        isDoorOpen = !isDoorOpen;
        float targetAngle = isDoorOpen ? openAngle : closedAngle;
        float timeElapsed = 0f;

        Quaternion initialRotation = doorPivot.localRotation;
        Quaternion targetRotation = Quaternion.Euler(doorPivot.localRotation.eulerAngles.x, targetAngle, doorPivot.localRotation.eulerAngles.z);

        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;  // Normaliza el tiempo transcurrido
            doorPivot.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t);  // Interpola entre la rotación inicial y la rotación objetivo
            timeElapsed += Time.deltaTime;  // Actualiza el tiempo transcurrido
            yield return null;  // Espera hasta el próximo frame
        }
        Debug.Log("Door Pivot ahora");
        doorPivot.localRotation = targetRotation;  // Asegura que la rotación final sea exacta
        Debug.Log("Enviando ahora la info a la rutina de SendDoorState");
        SendDoorState();
    }

    private void SendDoorState()
    {
        string doorState = isDoorOpen ? "Abierta" : "Cerrada";
        string message = "Puertas:Estado de la puerta: " + doorState + ", Habitación: " + this.gameObject.name;
        TcpClient client = new TcpClient("127.0.0.1", 8052);
        byte[] data = Encoding.ASCII.GetBytes(message);
        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        client.Close();
    }
}
