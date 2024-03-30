using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class DoorController : MonoBehaviour
{
    public string nombreComponente;
    public Transform doorPivot;  // La visagra o punto de rotación de la puerta
    private bool isDoorOpen = false;
    private bool isPlayerNearby = false;
    public float openAngle = -90f;
    private float closedAngle = 0f;
    private float animationTime = 2f;  // Duración de la animación en segundos
    public TMP_Text interactionPrompt;  // Referencia al objeto Text
    public TMP_Text doorSensorPromt;
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
        
        if (doorSensorPromt == null)
        {
            Debug.LogError("No se ha asignado un objecto Text a informationPrompt");
        }
        interactionPrompt.gameObject.SetActive(false);  // Oculta el texto al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerNearby = true;
        interactionPrompt.gameObject.SetActive(true);  // Muestra el texto
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerNearby = false;
        interactionPrompt.gameObject.SetActive(false);  // Oculta el texto
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ToggleDoorState());
            }
        }

        doorSensorPromt.text = this.gameObject.name + ':' + isDoorOpen;
    }

    private IEnumerator ToggleDoorState()
    {
        var doorDistance = 113;
        // Inicialización de las variables
        isDoorOpen = !isDoorOpen;
        var targetAngle = isDoorOpen ? openAngle : closedAngle;
        var timeElapsed = 0f;

        switch (this.gameObject.name)
        {
            // Como aquí tendremos el movimiento de las puertas, en la terraza irá diferente, ya que se desplazarán en el eje x
            case "Puerta1Terraza":
            {
                Vector3 initialPos = doorPivot.localPosition;
                Vector3 targetPos;
                if (!isDoorOpen)
                {
                    targetPos = new Vector3(initialPos.x - doorDistance, initialPos.y, initialPos.z);
                    isDoorOpen = false;
                }
                else
                {
                    targetPos = new Vector3(initialPos.x + doorDistance, initialPos.y, initialPos.z);
                    isDoorOpen = true;
                }
                while (timeElapsed < animationTime)
                {
                    float t = timeElapsed / animationTime;  // Normaliza el tiempo transcurrido
                    doorPivot.localPosition = Vector3.Lerp(initialPos, targetPos, t);  // Interpola entre la posición inicial y la posición objetivo
                    timeElapsed += Time.deltaTime;  // Actualiza el tiempo transcurrido
                    yield return null;  // Espera hasta el próximo frame
                }
                doorPivot.localPosition = targetPos;  // Asegura que la posición final sea exacta
                //SendDoorState();
                break;
            }
            case "Puerta2Terraza":
            {
                Vector3 initialPos = doorPivot.localPosition;
                Vector3 targetPos;
                if (!isDoorOpen)
                {
                    targetPos = new Vector3(initialPos.x + doorDistance, initialPos.y, initialPos.z);
                    isDoorOpen = false;
                }
                else
                {
                    targetPos = new Vector3(initialPos.x - doorDistance, initialPos.y, initialPos.z);
                    isDoorOpen = true;
                }
                while (timeElapsed < animationTime)
                {
                    float t = timeElapsed / animationTime;  // Normaliza el tiempo transcurrido
                    doorPivot.localPosition = Vector3.Lerp(initialPos, targetPos, t);  // Interpola entre la posición inicial y la posición objetivo
                    timeElapsed += Time.deltaTime;  // Actualiza el tiempo transcurrido
                    yield return null;  // Espera hasta el próximo frame
                }
                doorPivot.localPosition = targetPos;  // Asegura que la posición final sea exacta
                //SendDoorState();
                break;
            }
            default:
            {
                // En cambio, si es una puerta normal y corriente, se desplazará en ángulo de 90 grados para mostrar la apertura
                var localRotation = doorPivot.localRotation;
                Quaternion initialRotation = localRotation;
                Quaternion targetRotation = Quaternion.Euler(localRotation.eulerAngles.x, targetAngle, localRotation.eulerAngles.z);

                while (timeElapsed < animationTime)
                {
                    var t = timeElapsed / animationTime;  // Normaliza el tiempo transcurrido
                    doorPivot.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t);  // Interpola entre la rotación inicial y la rotación objetivo
                    timeElapsed += Time.deltaTime;  // Actualiza el tiempo transcurrido
                    yield return null;  // Espera hasta el próximo frame
                }
                doorPivot.localRotation = targetRotation;  // Asegura que la rotación final sea exacta
                SendDoorState();
                break;
            }
        }
        Debug.Log("Estado de la puerta " + nombreComponente + ": " + isDoorOpen);
    }

    private void SendDoorState()
    {
        var doorState = isDoorOpen ? "Abierta" : "Cerrada";
        var message = "Puertas:Estado de la puerta: " + doorState + ", Habitación: " + nombreComponente;
        TcpClient client = new TcpClient("127.0.0.1", 8052);
        byte[] data = Encoding.ASCII.GetBytes(message);
        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        client.Close();
    }
}
