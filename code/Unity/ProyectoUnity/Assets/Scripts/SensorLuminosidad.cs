using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
public class SensorLuminosidad : MonoBehaviour
{
    public Light[] luces;
    public float luminosidadMinima = 10f;

    public float tiempoApagado = 5f;
    private ISensorDataReciever _dataReciever;
    
    private float currentLuminosity;
    public DetectionSensor detectionSensor; // Referencia al sensor de movimiento
    private float tiempoSinMovimiento;
    private CicloDN cicloDN;
    private float luminosidad;
    private void Start()
    {
        luminosidad = 0;
        cicloDN = FindObjectOfType<CicloDN>(); // Obtener el script CicloDN
        if (cicloDN == null)
        {
            Debug.LogError("No se encontró un componente CicloDN en la escena.");
        }
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        if (GetComponent<BoxCollider>() == null)
        {
            Debug.LogError("El sensor de luminosidad no tiene un BoxCollider.");
            return;
        }

        detectionSensor = transform.parent.GetComponentInChildren<DetectionSensor>();
        if (detectionSensor == null)
        {
            Debug.LogError("El sensor de luminosidad no tiene un componente DetectionSensor.");
        }
    }

    void Update()
    {
        bool hayJugadorDentro = detectionSensor != null && detectionSensor._playerInside;
        foreach (Light luz in luces)
        {
            if (cicloDN != null && !luz.enabled)
            {
                luminosidad = cicloDN.IntensidadLuminica;
            }
            if (hayJugadorDentro && !(CicloDN.Hora >= 8 && CicloDN.Hora <= 20))
            {
                // Encender la luz si hay poca luz y el jugador está dentro
                luz.intensity = luminosidadMinima;
                luz.enabled = true;
                luminosidad = luz.intensity;
                tiempoSinMovimiento = 0f; // Reiniciar el contador si el jugador está dentro
            }
            else if (luz.intensity > 0f && !hayJugadorDentro)
            {
                // Reducir la intensidad gradualmente si la luz está encendida y el jugador NO está dentro
                tiempoSinMovimiento += Time.deltaTime;
                if (tiempoSinMovimiento >= tiempoApagado)
                {
                    luz.enabled = false;
                }
            }
        }
        _dataReciever.RecieveLuminosidadData(luminosidad, true);
    }
}
