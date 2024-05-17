using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
public class SensorLuminosidad : MonoBehaviour
{
    public Light[] luces;
    public float luminosidadMinima = 20f;
    public float velocidadIncremento = 0.001f;
    public float tiempoApagado = 5f;

    private float currentLuminosity;
    public DetectionSensor detectionSensor; // Referencia al sensor de movimiento
    private float tiempoSinMovimiento;

    private void Start()
    {
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
        Vector3 probePosition = GetComponent<BoxCollider>().bounds.center;
        LightProbes.GetInterpolatedProbe(probePosition, null, out SphericalHarmonicsL2 sh);

        currentLuminosity = 0.2126f * sh[0, 0] + 0.7152f * sh[1, 0] + 0.0722f * sh[2, 0];

        bool hayJugadorDentro = detectionSensor != null && detectionSensor._playerInside;

        foreach (Light luz in luces)
        {
            Debug.Log("HayJugadorDentro: " + hayJugadorDentro + " intensidad: " + luz.intensity);

            if (currentLuminosity < luminosidadMinima && hayJugadorDentro)
            {
                // Encender la luz si hay poca luz y el jugador está dentro
                luz.intensity = luminosidadMinima;
                luz.enabled = true;
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
    }
}
