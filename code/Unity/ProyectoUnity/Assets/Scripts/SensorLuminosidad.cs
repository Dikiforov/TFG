using System.Collections;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    private ISensorDataReciever _dataReciever;
    public Light[] lights; // Array para referenciar todas las luces de la habitación

    private void Start()
    {
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        
        // Asegúrate de asignar las luces al array en el Inspector de Unity
        if (lights.Length == 0) 
        {
            Debug.LogError("No se han asignado luces al sensor.");
        }
    }

    private void Update()
    {
        float totalIntensity = 0f;

        foreach (Light light in lights)
        {
            totalIntensity += light.intensity;
        }

        float averageIntensity = totalIntensity / lights.Length;
        _dataReciever.RecieveLuminosidadData(averageIntensity, true); 
    }
}
