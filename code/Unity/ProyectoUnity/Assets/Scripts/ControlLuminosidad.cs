using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public LightSensor lightSensor;
    public float minimumVisibleLuminosity = 2f;
    public List<Light> lightsToControl;
    public bool movementSensor = false;
    private void Update()
    {
        if (lightSensor.AverageLuminosity < minimumVisibleLuminosity && movementSensor)
        {
            foreach (Light light in lightsToControl)
            {
                light.intensity = minimumVisibleLuminosity;
            }
        }
        else
        {
            foreach (Light light in lightsToControl)
            {
                light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime * 5);
            }
        }
    }
}