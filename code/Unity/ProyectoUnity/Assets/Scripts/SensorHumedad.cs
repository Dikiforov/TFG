using UnityEngine;

public class SensorHumedad : MonoBehaviour
{
    public float humedadActual;
    private float _humedadAnterior;
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
        _humedadAnterior = humedadActual;
        _dataReciever = GetComponentInParent<ISensorDataReciever>();
    }

// Update is called once per frame
    void Update()
    {
        humedadActual = CicloDN.HumedadActual;
        _horaActual = CicloDN.Hora;
        float hActualRedondeada = Mathf.Round(humedadActual * 10.0f) * 0.1f;
        float hAnteriorRedondeada = Mathf.Round(_humedadAnterior * 10.0f) * 0.1f;
    
        bool enviarData = false;
        if (hAnteriorRedondeada != hActualRedondeada)
            enviarData = true;
        if (_horaActual >= 12 && _horaActual < 16) 
            enviarData = true;
        if (enviarData)
        {
            _humedadAnterior = humedadActual;
            _dataReciever.RecieveHumedadData(hActualRedondeada, enviarData, nombrePlacaPadre, CicloDN.fechaActual);
        }
    }
}
