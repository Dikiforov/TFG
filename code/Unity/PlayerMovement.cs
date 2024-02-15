using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cameraTransform;
    public float cameraDistance = 5.0f;
    public float sensitivity = 2.0f;

    private float xRotation = 0f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // Esconde y bloquea el cursor al centro de la pantalla.
        LockCursor();
    }

    private void Update()
    {
        // Si presionas la tecla Escape, libera el cursor (esto es opcional, pero útil durante el desarrollo)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
        MovePlayer();
        RotateCamera();
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // La dirección de movimiento ahora se basa en la orientación de la cámara
        Vector3 moveDirection = new Vector3(x, 0, z).normalized;
        Vector3 move = cameraTransform.right * moveDirection.x + cameraTransform.forward * moveDirection.z;
        move.y = 0; // Para evitar que el jugador se mueva verticalmente

        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        cameraTransform.position = transform.position - cameraTransform.forward * cameraDistance;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor en el centro de la ventana
        Cursor.visible = false;                    // Oculta el cursor
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;    // Libera el cursor
        Cursor.visible = true;                     // Muestra el cursor
    }
}
