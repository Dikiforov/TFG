using System;
using System.Collections;
using UnityEngine;

public class DetectionSensor : MonoBehaviour
{
    private PlayerMovementTracker _playerMovementTracker;
    private bool _playerInside;
    private Coroutine _checkPlayerCoroutine; // Referencia al coroutine para detenerlo si el jugador sale del trigger
    private ISensorDataReciever _dataReciever;

    private void Start()
    {
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        _playerMovementTracker = FindObjectOfType<PlayerMovementTracker>();
        if (_playerMovementTracker == null)
        {
            Debug.LogError("No se encontró el componente PlayerMovementTracker");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;
        }
    }

    private void Update()
    {
        if (_playerInside && _playerMovementTracker.IsMoving)
        {
            _dataReciever.RecieveMovimientoData(true);
        }
        else
        {
            _dataReciever.RecieveMovimientoData(false);
        }
    }

    /*private IEnumerator CheckPlayerMovement()
    {
        while (_playerInside)
        {
            yield return new WaitForSeconds(1f);
            // Si el jugador se está moviendo, enviamos 'true' al padre
            Debug.Log(_playerMovementTracker.IsMoving);
            _dataReciever.RecieveMovimientoData(_playerMovementTracker.IsMoving);
        }
    }*/
}