using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDN : MonoBehaviour
{
    public float Hora = 0;
    public Transform Sol;
    public float duracionDiaMin = 1;
    private float SolX;
    private float IniSolX;
    void Start()
    {
        IniSolX = (Hora * (-90)) * 12;
        Sol.localEulerAngles = new Vector3(IniSolX, 0, 0);
    }
    void Update()
    {

        Hora += Time.deltaTime * (24 / (60 * duracionDiaMin));
        if (Hora >= 24) Hora = 0;
        rotacionSol();
    }
    void rotacionSol()
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
