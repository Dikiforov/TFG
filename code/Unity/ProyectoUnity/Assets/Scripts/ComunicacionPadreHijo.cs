public interface ISensorDataReciever
{
    void RecieveTempData(float temp, bool enviarData);
    void RecieveDoorState(bool isOpen, string doorName);
    void RecieveHumedadData(float humedad, bool enviarData);
    void RecieveLuminosidadData(float luminosidad, bool enviarData);
    void RecieveMovimientoData(bool movement);
    void RecieveSonidoData(float sound, bool enviarData);
    void RecievePresionData(float presion, bool enviarData);
}
