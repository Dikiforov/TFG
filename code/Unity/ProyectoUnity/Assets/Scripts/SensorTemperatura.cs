using UnityEngine;

public class SensorTemperatura : MonoBehaviour
{
    public float temperaturaActual;
    private float _temperaturaAnterior;
    private float _horaActual;
    
    public CicloDN cicloDn;
    
    // Start is called before the first frame update
    void Start()
    {
        cicloDn = FindObjectOfType<CicloDN>();
        _temperaturaAnterior = temperaturaActual;
    }

    // Update is called once per frame
    void Update()
    {
        
        temperaturaActual = CicloDN.TempActual;
        _horaActual = CicloDN.Hora;
        float tActualRedondeada = Mathf.Round(temperaturaActual * 100.0f) * 0.01f;
        float tAnteriorRedondeada = Mathf.Round(_temperaturaAnterior * 100.0f) * 0.01f;
        if (tAnteriorRedondeada != tActualRedondeada)
        {
            _temperaturaAnterior = temperaturaActual;
            Debug.Log("Hora: " +_horaActual +"tActualRedondeada: " + tActualRedondeada + "tAnteriorRedondeada: " + tAnteriorRedondeada);
        }

    }
}
