using UnityEngine;
public class SensorTemperatura : MonoBehaviour
{
    public float temperaturaActual;
    private float _temperaturaAnterior;
    private float _horaActual;
    public string nombreComponente;
    public CicloDN cicloDn;

    private ISensorDataReciever _dataReciever;
    
    // Start is called before the first frame update
    void Start()
    {
        cicloDn = FindObjectOfType<CicloDN>();
        _temperaturaAnterior = temperaturaActual;
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
    }

    // Update is called once per frame
    void Update()
    {
        temperaturaActual = CicloDN.TempActual;
        _horaActual = CicloDN.Hora;
        float tActualRedondeada = Mathf.Round(temperaturaActual * 100.0f) * 0.01f;
        float tAnteriorRedondeada = Mathf.Round(_temperaturaAnterior * 100.0f) * 0.01f;
        
        bool enviarData = false;
        if (tAnteriorRedondeada != tActualRedondeada)
            enviarData = true;
        if (_horaActual >= 12 && _horaActual < 16) 
            enviarData = true;
        //Debug.Log("Hora:" + _horaActual + "enviar_datos: " + enviarData);
        Debug.Log("Ant: "+ tAnteriorRedondeada + "- Act:" + tActualRedondeada + "- Enviar: " + enviarData + "Hora:" + _horaActual);
        if (enviarData)
        {
            _temperaturaAnterior = temperaturaActual;
            _dataReciever.RecieveTempData(tActualRedondeada, enviarData);
        }
    }
}
