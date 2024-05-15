using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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

        _checkPlayerCoroutine = StartCoroutine(CheckPlayerMovement());
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

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator CheckPlayerMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_playerInside)
            {
                _dataReciever.RecieveMovimientoData(_playerMovementTracker.IsMoving);
            }
        }
    }
}