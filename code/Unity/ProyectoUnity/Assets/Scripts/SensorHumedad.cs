using UnityEngine;

public class SensorHumedad : MonoBehaviour
{
    public float humedadBase = 65f; // Humedad promedio general de Tarragona
    public float variacionManana = 15f; // Aumento de humedad por la mañana
    public float variacionMediodia = -10f; // Disminución de humedad al mediodía

    private ISensorDataReciever _dataReciever;
    private float _ultimaHumedadEnviada;

    private string nombrePlacaPadre; // Variable para almacenar el nombre del padre

    // Start is called before the first frame update
    void Start()
    {
        nombrePlacaPadre = transform.parent.name;
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
        _ultimaHumedadEnviada = 0f; // Inicializar para forzar el primer envío
    }

    private void Update()
    {
        float hora = CicloDN.Hora;
        float humedadActual = CalcularHumedad(hora);

        // Redondear a una cifra decimal
        humedadActual = Mathf.Round(humedadActual * 10f) / 10f;

        // Enviar datos si la humedad ha cambiado significativamente
        if (Mathf.Abs(humedadActual - _ultimaHumedadEnviada) >= 0.1f)
        {
            _dataReciever.RecieveHumedadData(humedadActual, true, nombrePlacaPadre);
            _ultimaHumedadEnviada = humedadActual;
        }
    }

    private float CalcularHumedad(float hora)
    {
        // Curva de humedad a lo largo del día
        if (hora >= 0 && hora < 6)
        {
            // Aumento gradual de humedad por la mañana
            return Mathf.Lerp(humedadBase, humedadBase + variacionManana, hora / 6f);
        }
        else if (hora >= 6 && hora < 10)
        {
            // Disminución gradual de humedad hasta el mediodía
            return Mathf.Lerp(humedadBase + variacionManana, humedadBase, (hora - 6f) / 4f);
        }
        else if (hora >= 10 && hora < 16)
        {
            // Disminución adicional de humedad al mediodía
            return Mathf.Lerp(humedadBase, humedadBase + variacionMediodia, (hora - 10f) / 6f);
        }
        else
        {
            // Aumento gradual de humedad por la tarde y noche
            return Mathf.Lerp(humedadBase + variacionMediodia, humedadBase, (hora - 16f) / 8f);
        }
    }
}
