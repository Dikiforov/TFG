using UnityEngine;
using System;

public class Puerta : MonoBehaviour
{
    public bool puertaAbierta; // Variable para el estado de la puerta (abierta/cerrada)
    private string _nombrePuerta; // Identificador de la puerta
    public Transform posicionPuerta; // Posición de la puerta en la habitación

    private void Start()
    {
        _nombrePuerta = this.gameObject.name;
        puertaAbierta = false;
    }

    private void Update()
    {
        EnviarMensajePuerta();
    }

    private void Awake()
    {
        puertaAbierta = false; // Inicializa la puerta como cerrada
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if (other.CompareTag("Player")) // Detecta la entrada del usuario
        {
            puertaAbierta = true; // Cambia el estado de la puerta a abierta
            EnviarMensajePuerta(); // Envía un mensaje al padre con el cambio de estado
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("B");
        if (other.CompareTag("Player")) // Detecta la salida del usuario
        {
            puertaAbierta = false; // Cambia el estado de la puerta a cerrada
            EnviarMensajePuerta(); // Envía un mensaje al padre con el cambio de estado
        }
    }

    private void EnviarMensajePuerta()
    {
        // Obtenemos la referencia al objeto padre (habitación)
        Transform padre = transform.parent;

        // Creamos un mensaje con la información de la puerta
        MensajePuerta mensaje = new MensajePuerta(_nombrePuerta, puertaAbierta, posicionPuerta.position, DateTime.Now);

        // Enviamos el mensaje al padre
        padre.gameObject.SendMessage(_nombrePuerta + "->" + mensaje);
        Debug.Log(_nombrePuerta + "->" + mensaje);
    }
}

public struct MensajePuerta
{
    public string NombrePuerta;
    public bool PuertaAbierta;
    public Vector3 PosicionPuerta;
    public DateTime Hora;

    public MensajePuerta(string nombrePuerta, bool puertaAbierta, Vector3 posicionPuerta, DateTime hora)
    {
        this.NombrePuerta = nombrePuerta;
        this.PuertaAbierta = puertaAbierta;
        this.PosicionPuerta = posicionPuerta;
        this.Hora = hora;
    }
}