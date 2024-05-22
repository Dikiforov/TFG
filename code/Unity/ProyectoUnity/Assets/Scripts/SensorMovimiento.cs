using UnityEngine;

public class DetectionSensor : MonoBehaviour
{
    private PlayerMovementTracker _playerMovementTracker;
    private BoxCollider _sensorCollider; // Referencia al BoxCollider del sensor
    private ISensorDataReciever _dataReciever;
    private DetectionSensor sensorMovimiento;
    public bool _hayMovimientoCache; 
    
    // Propiedad pública para acceder al estado de movimiento
    public bool _playerInside; 
    private string nombrePlacaPadre; // Variable para almacenar el nombre del padre

    // Start is called before the first frame update
    void Start()
    {
        nombrePlacaPadre = transform.parent.name;
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        _playerMovementTracker = FindObjectOfType<PlayerMovementTracker>();
        _sensorCollider = GetComponent<BoxCollider>(); // Obtener el BoxCollider

        if (_playerMovementTracker == null)
        {
            Debug.LogError("No se encontró el componente PlayerMovementTracker");
        }

        if (_sensorCollider == null)
        {
            Debug.LogError("El sensor de movimiento no tiene un BoxCollider.");
        }
    }

    private void Update()
    {
        if (_sensorCollider != null && _playerMovementTracker != null) 
        {
            // Verificar si el jugador está dentro del área del sensor
            _playerInside = _sensorCollider.bounds.Contains(_playerMovementTracker.transform.position);

            // Actualizar la variable _hayMovimientoCache
            _hayMovimientoCache = _playerInside && _playerMovementTracker.IsMoving;
        }
        _dataReciever.RecieveMovimientoData(_hayMovimientoCache, nombrePlacaPadre);
    }
}