using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CicloDN : MonoBehaviour
{
    // Variables públicas selecciondas por el usuario
    public Transform Sol; // Contendrá el objeto Sol que irá rotando alrededor del objeto del domicilio
    public float DuracionDiaMin = 1;    // Tiempo, en minutos, que durará un día de 24 horas

    public enum Estaciones    // Estación seleccionada por el usuario
    {
        Invierno,
        Primavera,
        Verano,
        Otoño
    };

    public Estaciones EstacionSeleccionada = Estaciones.Invierno;   // De serie invierno
    
    // Variables privadas para el movimiento del Sol y el cálculo de la hora
    private float Hora = 0;
    private float SolX;
    private float IniSolX;
    
    // Variable privadas para la gestión de la temperatura
    private float TempMinima;
    private float TempMaxima;
    private float TempActual;
    private float[,] temperaturas = new float[,]    // Máximas y mínimas en base a la estación
{
        {12.5f, 7.8f},    // Invierno
        {20.1f, 14.8f},   // Primavera
        {27.3f, 22.4f},   // Verano
        {16.4f, 12.1f},   // Otoño
};
    
    void Start()
    {
        // Inicio de las temperaturas en base a la estación seleccionada
        Debug.Log("Pre seleccion");
        Debug.Log(EstacionSeleccionada);
        TempMaxima = temperaturas[(int)EstacionSeleccionada, 0];
        TempMinima = temperaturas[(int)EstacionSeleccionada, 1];
        TempActual = TempMinima;
        // Inicio del sol en base a la hora, predeterminada a las 00:00
        IniSolX = (Hora * (-90)) * 12;
        Sol.localEulerAngles = new Vector3(IniSolX, 0, 0);
    }
    void Update()
    {
        // Incremento del tiempo en base
        Hora += Time.deltaTime * (24 / (60 * DuracionDiaMin));
        if (Hora >= 24)
        {
            // Cada 24 horas se reinicia la hora a las 00:00
            Hora = 0;
            // Recalcular las temperaturas para tener un registros más distintivo
            TempMaxima += 0.2f * (Random.value > 0.5f ? 1 : -1);
            TempMinima += 0.2f * (Random.value > 0.5f ? 1 : -1);
            TempActual = TempMinima;
        }

        float CurvaTemp = 2f;
        if (Hora < 8) CurvaTemp =  (2 / 8);    // Hacemos que suba 2 grados en el intervalo de las 00-08
        else if (Hora < 12) CurvaTemp = (TempMaxima - TempActual) / 4;  // Subimos lo que quede hasta el máximo en  horas
            else if (Hora < 16) CurvaTemp = 0;  // Que no haya modificación de temperatura en las horas máximas
                else if (Hora < 23.59) CurvaTemp = -(TempMaxima - TempMinima) / 8; // Decremento de temperatura
        TempActual += CurvaTemp;
        Debug.Log(Hora + "(" + TempActual + ")" + "Curva temp: " + CurvaTemp);
        RotacionSol();
    }
    // Zona de movimiento del Sol
    void RotacionSol()
    {
        SolX = 15 * Hora;   // 15 porque es los grados del sol (24 h * 15 grados = 360 grados)
        Sol.localEulerAngles = new Vector3(SolX, 0, 0);
        if (Hora < 6 || Hora > 20)
        {
            Sol.GetComponent<Light>().intensity = 0;
        }
        else
        {
            Sol.GetComponent<Light>().intensity = 1;
        }
    }
}
