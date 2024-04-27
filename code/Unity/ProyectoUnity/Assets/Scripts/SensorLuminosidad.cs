using System.Collections.Generic;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private int minNumberOfLights = 3;

    private List<Light> lights = new List<Light>();

    public float AverageLuminosity => CalculateAverageLuminosity();

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectLights();
            CalculateAverageLuminosity();
        }
    }

    private void CollectLights()
    {
        lights.Clear();
        Collider[] lightsInRange = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider lightCollider in lightsInRange)
        {
            Light lightComponent = lightCollider.GetComponent<Light>();
            if (lightComponent != null && lightComponent.type == LightType.Spot)
            {
                lights.Add(lightComponent);
            }
        }
    }

    private float CalculateAverageLuminosity()
    {
        if (lights.Count < minNumberOfLights) return 0;

        float totalLuminosity = 0;

        foreach (Light light in lights)
        {
            totalLuminosity += light.intensity;
        }

        float averageLuminosity = totalLuminosity / lights.Count;
        return averageLuminosity;
    }
}