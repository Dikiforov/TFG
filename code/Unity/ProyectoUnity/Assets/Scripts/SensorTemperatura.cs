using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using TMPro;
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
        if (tAnteriorRedondeada != tActualRedondeada)
        {
            _temperaturaAnterior = temperaturaActual;
            //Debug.Log("Hora: " +_horaActual +"tActualRedondeada: " + tActualRedondeada + "tAnteriorRedondeada: " + tAnteriorRedondeada);
            _dataReciever.RecieveTempData(tActualRedondeada);
            //SendTemperature(tActualRedondeada);
        }
    }

    private void SendTemperature(float temperatura)
    {
        var message = "Temperatura:" + temperatura + " C en " + nombreComponente;
        TcpClient client = new TcpClient("127.0.0.1", 8052);
        byte[] data = Encoding.ASCII.GetBytes(message);
        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        client.Close();
    }
}
