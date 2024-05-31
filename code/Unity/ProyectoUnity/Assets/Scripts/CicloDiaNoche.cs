using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CicloDN : MonoBehaviour
{
    public enum Estaciones
    {
        Invierno,
        Primavera,
        Verano,
        Otoño
    };

    public Estaciones EstacionSeleccionada = Estaciones.Invierno;
    public static float Hora = 0;
    public static TimeSpan horaFormateada;
    private float SolX;
    private float IniSolX;

    private float TempMinima;
    private float TempMaxima;
    public static float TempActual;
    private float[,] temperaturas = new float[,]
    {
        {12.5f, 7.8f},
        {20.1f, 14.8f},
        {27.3f, 22.4f},
        {16.4f, 12.1f},
    };

    private float HumedadMinima;
    private float HumedadMaxima;
    public static float HumedadActual;
    private float[,] humedades = new float[,]
    {
        {75f, 65f}, // Invierno (media: 70%)
        {65f, 55f}, // Primavera (media: 60%)
        {60f, 50f}, // Verano (media: 55%)
        {70f, 60f}, // Otoño (media: 65%)
    };
    
    public GameObject Sol;
    public Light luzSolar; // Referencia al componente Light del sol
    public float intensidadMaxima; // Intensidad máxima de la luz solar al mediodía
    public float intensidadMinima; // Intensidad mínima de la luz solar durante la noche
    public float IntensidadLuminica; // Propiedad pública para acceder a la intensidad

    public float DuracionDiaMin;

    void Start()
    {
        TempMaxima = temperaturas[(int)EstacionSeleccionada, 0];
        TempMinima = temperaturas[(int)EstacionSeleccionada, 1];
        TempActual = TempMinima;
        
        HumedadMaxima = humedades[(int)EstacionSeleccionada, 0];
        HumedadMinima = humedades[(int)EstacionSeleccionada, 1];
        HumedadActual = HumedadMinima;
        
        luzSolar = Sol.GetComponent<Light>();
        if (luzSolar == null)
        {
            Debug.LogError("El GameObject del sol no tiene un componente Light.");
        }
        IniSolX = (Hora * (-90)) * 12;
        Sol.transform.localEulerAngles = new Vector3(IniSolX, 0, 0);
    }

    void Update()
    {
        Hora += Time.deltaTime * (24 / (60 * DuracionDiaMin)); // Obtención de la hora en base a la duración del día
        horaFormateada = TimeSpan.FromHours(Hora);
        if (Hora >= 24)
        {
            NuevosDatosHumedadTemperatura();
        }

        AjustarIntensidadLuzSolar();
        TempActual = CalculoTemperatura();
        HumedadActual = CalculoHumedad();
        RotacionSol();
    }
    void AjustarIntensidadLuzSolar()
    {
        if (luzSolar != null)
        {
            // Calcular la intensidad de la luz solar en función de la hora
            float t = Mathf.InverseLerp(0f, 24f, Hora); // Normalizar la hora a un valor entre 0 y 1
            float intensidad = Mathf.Lerp(intensidadMinima, intensidadMaxima, Mathf.Sin(t * Mathf.PI));
            
            // Redondear a un decimal y asignar a la luz solar y a la propiedad IntensidadLuminica
            intensidad = Mathf.Round(intensidad * 10f) / 10f; 
            luzSolar.intensity = intensidad;
            IntensidadLuminica = intensidad;
        }
    }
    
    void RotacionSol()
    {
        SolX = 15 * Hora;
        Sol.transform.localEulerAngles = new Vector3(SolX, 0, 0);
    }

    float CalculoTemperatura()
    {
        float aux = 0;
        if (Hora >= 0 && Hora < 4 && TempActual < TempMinima + 1)
        {
            aux = Mathf.Lerp(TempMinima, TempMinima + 1, (Hora - 0) / 4);
        }
        else if (Hora >= 4 && Hora < 8 && TempActual < TempMinima + 2)
        {
            aux = Mathf.Lerp(TempMinima, TempMinima + 2, (Hora - 4) / 4);
        }
        else if (Hora >= 8 && Hora < 12)
        {
            aux = Mathf.Lerp(TempMinima + 3, TempMaxima, (Hora - 8) / 4);
        }
        else if (Hora >= 12 && Hora < 16)
        {
            aux = TempMaxima;
        }
        else if (Hora >= 16 && Hora < 24)
        {
            aux = Mathf.Lerp(TempMaxima, TempMinima, (Hora - 16) / 8);
        }

        return aux;
    }
    
    float CalculoHumedad()
    {
        // Lógica similar a CalculoTemperatura, pero ajustada para humedad
        float aux = 0;
        if (Hora >= 0 && Hora < 6 && HumedadActual < HumedadMinima + 2)
        {
            aux = Mathf.Lerp(HumedadMinima, HumedadMinima + 2, (Hora - 0) / 6);
        }
        else if (Hora >= 6 && Hora < 12)
        {
            aux = Mathf.Lerp(HumedadMinima + 2, HumedadMaxima, (Hora - 6) / 6);
        }
        else if (Hora >= 12 && Hora < 18)
        {
            aux = HumedadMaxima;
        }
        else if (Hora >= 18 && Hora < 24)
        {
            aux = Mathf.Lerp(HumedadMaxima, HumedadMinima, (Hora - 18) / 6);
        }

        return aux;
    }
    
    void NuevosDatosHumedadTemperatura()
    {
        Hora = 0;
        TempMaxima += 0.2f * (Random.value > 0.5f ? 1 : -1);
        TempMinima += 0.2f * (Random.value> 0.5f ? 1 : -1);
        TempActual = TempMinima;
        HumedadMaxima += 0.5f * (Random.value > 0.5f ? 1 : -1);
        HumedadMinima += 0.5f * (Random.value > 0.5f ? 1 : -1);
        HumedadActual = HumedadMinima;
    }
}
