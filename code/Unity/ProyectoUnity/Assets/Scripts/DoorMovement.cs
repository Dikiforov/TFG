using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorController : MonoBehaviour
{
    public string nombreComponente;
    public Transform doorPivot;  // La visagra o punto de rotación de la puerta
    private bool _isDoorOpen;
    private bool _isPlayerNearby;
    public float openAngle = -90f;
    private float _closedAngle = 0f;
    private float _animationTime = 2f;  // Duración de la animación en segundos
    private ISensorDataReciever _dataReciever;

    private void Start()
    {
        if (doorPivot == null)
        {
            Debug.LogError("No se ha asignado una visagra a la puerta.");
        }
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isPlayerNearby = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNearby)
        { 
            StartCoroutine(ToggleDoorState());
        }
    }

    private IEnumerator ToggleDoorState()
    {
        var doorDistance = 113;
        // Inicialización de las variables
        _isDoorOpen = !_isDoorOpen;
        var targetAngle = _isDoorOpen ? openAngle : _closedAngle;
        var timeElapsed = 0f;

        switch (nombreComponente)
        {
            // Como aquí tendremos el movimiento de las puertas, en la terraza irá diferente, ya que se desplazarán en el eje x
            case "Puerta1Terraza":
            {
                Vector3 initialPos = doorPivot.localPosition;
                Vector3 targetPos;
                if (!_isDoorOpen)
                {
                    targetPos = new Vector3(initialPos.x - doorDistance, initialPos.y, initialPos.z);
                    _isDoorOpen = false;
                }
                else
                {
                    targetPos = new Vector3(initialPos.x + doorDistance, initialPos.y, initialPos.z);
                    _isDoorOpen = true;
                }
                while (timeElapsed < _animationTime)
                {
                    float t = timeElapsed / _animationTime;  // Normaliza el tiempo transcurrido
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
                if (!_isDoorOpen)
                {
                    targetPos = new Vector3(initialPos.x + doorDistance, initialPos.y, initialPos.z);
                    _isDoorOpen = false;
                }
                else
                {
                    targetPos = new Vector3(initialPos.x - doorDistance, initialPos.y, initialPos.z);
                    _isDoorOpen = true;
                }
                while (timeElapsed < _animationTime)
                {
                    float t = timeElapsed / _animationTime;  // Normaliza el tiempo transcurrido
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

                while (timeElapsed < _animationTime)
                {
                    var t = timeElapsed / _animationTime;  // Normaliza el tiempo transcurrido
                    doorPivot.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t);  // Interpola entre la rotación inicial y la rotación objetivo
                    timeElapsed += Time.deltaTime;  // Actualiza el tiempo transcurrido
                    yield return null;  // Espera hasta el próximo frame
                }
                doorPivot.localRotation = targetRotation;  // Asegura que la rotación final sea exacta
                break;
            }
        }
        _dataReciever.RecieveDoorState(_isDoorOpen, nombreComponente);
    }
}
