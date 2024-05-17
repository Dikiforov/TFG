using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))] // Asegurarse de que el sensor tenga un BoxCollider
public class SensorLuminosidad : MonoBehaviour
{
    public Light[] luces;
    public float luminosidadMinima = 20f;
    public float velocidadIncremento = 1f;

    private float currentLuminosity;

    private void Start()
    {
        // Verificar si el componente BoxCollider existe
        if (GetComponent<BoxCollider>() == null)
        {
            Debug.LogError("El sensor de luminosidad no tiene un BoxCollider.");
            return;
        }
    }

    void Update()
    {
        // Obtener el centro del BoxCollider
        Vector3 probePosition = GetComponent<BoxCollider>().bounds.center;

        LightProbes.GetInterpolatedProbe(probePosition, null, out SphericalHarmonicsL2 sh);

        currentLuminosity = 0.2126f * sh[0, 0] + 0.7152f * sh[1, 0] + 0.0722f * sh[2, 0];

        Debug.Log("Luminosidad actual: " + currentLuminosity);

        foreach (Light luz in luces)
        {
            if (currentLuminosity < luminosidadMinima)
            {
                luz.intensity = Mathf.Min(luz.intensity + velocidadIncremento * Time.deltaTime, luminosidadMinima);
                luz.enabled = true;
            }
            else
            {
                luz.intensity = luminosidadMinima;
            }
            Debug.Log("\tLuz: "+luz.name+" tiene una intensidad de: "+luz.intensity);
        }
    }
}