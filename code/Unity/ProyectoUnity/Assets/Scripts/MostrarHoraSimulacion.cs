using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Referencia al componente TextMeshPro

    private void Start()
    {
        // Obtener la referencia al componente TextMeshPro
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Obtener la hora formateada del script CicloDN
        string horaFormateada = CicloDN.fechaActual.ToString();

        // Actualizar el texto del TextMeshPro
        //textMeshPro.text = "Hora: " + horaFormateada;
    }
}