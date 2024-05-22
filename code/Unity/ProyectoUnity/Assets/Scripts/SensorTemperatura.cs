using UnityEngine;
public class SensorTemperatura : MonoBehaviour
{
    public float temperaturaActual;
    private float _temperaturaAnterior;
    private float _horaActual;
    public string nombreComponente;
    public CicloDN cicloDn;

    private ISensorDataReciever _dataReciever;
    private string nombrePlacaPadre; // Variable para almacenar el nombre del padre

    // Start is called before the first frame update
    void Start()
    {
        nombrePlacaPadre = transform.parent.name;
        cicloDn = FindObjectOfType<CicloDN>();
        _temperaturaAnterior = temperaturaActual;
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
    }

    // Update is called once per frame
    void Update()
    {
        temperaturaActual = CicloDN.TempActual;
        _horaActual = CicloDN.Hora;
        float tActualRedondeada = Mathf.Round(temperaturaActual * 10.0f) * 0.1f;
        float tAnteriorRedondeada = Mathf.Round(_temperaturaAnterior * 10.0f) * 0.1f;
        
        bool enviarData = false;
        if (tAnteriorRedondeada != tActualRedondeada)
            enviarData = true;
        if (_horaActual >= 12 && _horaActual < 16) 
            enviarData = true;
        //Debug.Log("Hora:" + _horaActual + "enviar_datos: " + enviarData);
        if (enviarData)
        {
            //Debug.Log("Ant: "+ tAnteriorRedondeada + "- Act:" + tActualRedondeada + "- Enviar: " + enviarData + "Hora:" + _horaActual);
            _temperaturaAnterior = temperaturaActual;
            _dataReciever.RecieveTempData(tActualRedondeada, enviarData, nombrePlacaPadre);
        }
    }
}
