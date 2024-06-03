using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SensorLuminosidad : MonoBehaviour
{
    public Light[] luces;
    public float luminosidadMinima = 10f;

    public float tiempoApagado;
    private ISensorDataReciever _dataReciever;
    
    private float currentLuminosity;
    public DetectionSensor detectionSensor; // Referencia al sensor de movimiento
    private float tiempoSinMovimiento;
    private CicloDN cicloDN;
    private float luminosidad;
    private string nombrePlacaPadre; // Variable para almacenar el nombre del padre

    private TimeSpan horaAnterior;
    
    /*
     */
    // Start is called before the first frame update
    void Start()
    {
        nombrePlacaPadre = transform.parent.name;
        luminosidad = 0;
        cicloDN = FindObjectOfType<CicloDN>(); // Obtener el script CicloDN
        if (cicloDN == null)
        {
            Debug.LogError("No se encontró un componente CicloDN en la escena.");
        }
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        /*if (GetComponent<BoxCollider>() == null)
        {
            Debug.LogError("El sensor de luminosidad no tiene un BoxCollider.");
            return;
        }*/

        detectionSensor = transform.parent.GetComponentInChildren<DetectionSensor>();
        if (detectionSensor == null)
        {
            Debug.LogError("El sensor de luminosidad no tiene un componente DetectionSensor.");
        }

        tiempoApagado = (5f * ((CicloDN.DuracionDiaMin * 60f / 24f) / 3600f));
    }

    void Update()
    {
        bool hayJugadorDentro = detectionSensor != null && detectionSensor._playerInside;
        foreach (Light luz in luces)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hayJugadorDentro) 
            {
                    luz.enabled = false;
            }
            if (cicloDN != null && !luz.enabled)
            {
                luminosidad = cicloDN.IntensidadLuminica;
            }
            if (hayJugadorDentro && !(CicloDN.Hora >= 8 && CicloDN.Hora <= 20) && !luz.enabled)
            {
                // Encender la luz si hay poca luz y el jugador está dentro
                luz.intensity = luminosidadMinima;
                luz.enabled = true;
                luminosidad = luz.intensity;
                tiempoSinMovimiento = 0f; // Reiniciar el contador si el jugador está dentro
                horaAnterior = CicloDN.horaFormateada;
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
            if (hayJugadorDentro && (CicloDN.horaFormateada.Hours >= horaAnterior.Hours && CicloDN.horaFormateada.Minutes != horaAnterior.Minutes && (CicloDN.horaFormateada.Seconds - horaAnterior.Seconds) > 30))
            {
                luz.enabled = false;
            }
        }
        _dataReciever.RecieveLuminosidadData(luminosidad, true, nombrePlacaPadre, CicloDN.fechaActual);
    }
}
