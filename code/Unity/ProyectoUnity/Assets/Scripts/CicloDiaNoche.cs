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
    public static DateTime fechaActual = new DateTime(2024, 1, 1); // Comenzar el 1 de enero de 2024
    private float horaAmanecer = 6f;
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

    public static float DuracionDiaMin;
    public float duracionDiaPorMinutos;
    void Start()
    {
        if (duracionDiaPorMinutos <= 0) duracionDiaPorMinutos = 1;
        DuracionDiaMin = duracionDiaPorMinutos;
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
        fechaActual = fechaActual.Date + horaFormateada;
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
            float t = Mathf.InverseLerp(0f, 24f, Hora);
            float intensidad;

            if (Hora >= horaAmanecer && Hora < 12f) // Amanecer y mañana
            {
                intensidad = Mathf.Lerp(intensidadMinima, intensidadMaxima/2,
                    (Hora - horaAmanecer) / (12f - horaAmanecer));
            }
            else if (Hora >= 12f && Hora < 18f) // Mediodía y tarde
            {
                intensidad = intensidadMaxima/2;
            }
            else // Noche
            {
                intensidad = Mathf.Lerp(intensidadMaxima/2, intensidadMinima, (Hora - 18f) / (24f - 18f));
            }

            intensidad = (Mathf.Round(intensidad * 10f) / 10f)/2f;
            //luzSolar.intensity = intensidad;
            IntensidadLuminica = intensidad;

            // Ajustar color para un amanecer/atardecer más suave
            if (Hora >= horaAmanecer && Hora < 8f) // Amanecer suave
            {
                luzSolar.color = new Color(intensidad, intensidad * 0.8f, intensidad * 0.6f);
            }
            else if (Hora >= 18f && Hora < 20f) // Atardecer suave
            {
                luzSolar.color = new Color(intensidad, intensidad * 0.7f, intensidad * 0.5f);
            }
            else if (Hora >= 8f && Hora < 18f) // Día
            {
                luzSolar.color = new Color(intensidad, intensidad * 0.9f, intensidad * 0.8f); // Tono más cálido
            }
            else // Noche profunda
            {
                luzSolar.color = new Color(intensidad * 0.5f, intensidad * 0.5f, intensidad * 0.6f); // Tono azulado
            }
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
        fechaActual = fechaActual.AddDays(1);
        TempMaxima += 0.2f * (Random.value > 0.5f ? 1 : -1);
        TempMinima += 0.2f * (Random.value> 0.5f ? 1 : -1);
        TempActual = TempMinima;
        HumedadMaxima += 0.5f * (Random.value > 0.5f ? 1 : -1);
        HumedadMinima += 0.5f * (Random.value > 0.5f ? 1 : -1);
        HumedadActual = HumedadMinima;
    }
}
