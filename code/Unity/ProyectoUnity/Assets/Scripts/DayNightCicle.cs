using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float Hora = 0;
    private float SolX;
    private float IniSolX;

    private float TempMinima;
    private float TempMaxima;
    private float TempActual;
    private float[,] temperaturas = new float[,]
    {
        {12.5f, 7.8f},
        {20.1f, 14.8f},
        {27.3f, 22.4f},
        {16.4f, 12.1f},
    };

    public GameObject Sol;
    public float DuracionDiaMin = 1;

    void Start()
    {
        TempMaxima = temperaturas[(int)EstacionSeleccionada, 0];
        TempMinima = temperaturas[(int)EstacionSeleccionada, 1];
        TempActual = TempMinima;

        IniSolX = (Hora * (-90)) * 12;
        Sol.transform.localEulerAngles = new Vector3(IniSolX, 0, 0);
    }

    void Update()
    {
        Hora += Time.deltaTime * (24 / (60 * DuracionDiaMin));

        if (Hora >= 24)
        {
            NuevoDiaTemperaturas();
        }

        CalculoTemperatura();
        RotacionSol();
    }

    void RotacionSol()
    {
        SolX = 15 * Hora;
        Sol.transform.localEulerAngles = new Vector3(SolX, 0, 0);
    }

    void CalculoTemperatura()
    {
        if (Hora >= 0 && Hora < 4 && TempActual < TempMinima + 1)
        {
            TempActual = Mathf.Lerp(TempMinima, TempMinima + 1, (Hora - 0) / 4);
        }
        else if (Hora >= 4 && Hora < 8 && TempActual < TempMinima + 2)
        {
            TempActual = Mathf.Lerp(TempMinima, TempMinima + 2, (Hora - 4) / 4);
        }
        else if (Hora >= 8 && Hora < 12)
        {
            TempActual = Mathf.Lerp(TempMinima + 3, TempMaxima, (Hora - 8) / 4);
        }
        else if (Hora >= 12 && Hora < 16)
        {
            TempActual = TempMaxima;
        }
        else if (Hora >= 16 && Hora < 24)
        {
            TempActual = Mathf.Lerp(TempMaxima, TempMinima, (Hora - 16) / 8);
        }
    }

    void NuevoDiaTemperaturas()
    {
        Hora = 0;
        TempMaxima += 0.2f * (Random.value > 0.5f ? 1 : -1);
        TempMinima += 0.2f * (Random.value> 0.5f ? 1 : -1);
        TempActual = TempMinima;
    }
}