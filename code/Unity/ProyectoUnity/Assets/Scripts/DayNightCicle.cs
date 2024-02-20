using System.Collections;
using UnityEngine;

public class LightRotator : MonoBehaviour
{

    public Transform objectToOrbit; // Arrastra el objeto alrededor del cual deseas orbitar en el inspector
    public float rotationSpeed = 1.0f;

    private Quaternion initialRotation;

    void Start()
    {
        // Almacena la rotación inicial de la luz
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Gira la luz alrededor del objeto a una velocidad constante
        transform.RotateAround(objectToOrbit.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Aplica la rotación inicial a la luz en cada frame
        transform.rotation = initialRotation;
    }
}
