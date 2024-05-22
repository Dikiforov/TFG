public interface ISensorDataReciever
{
    void RecieveTempData(float temp, bool enviarData, string nombrePlaca);
    void RecieveDoorState(bool isOpen, string doorName, string nombrePlaca);
    void RecieveHumedadData(float humedad, bool enviarData, string nombrePlaca);
    void RecieveLuminosidadData(float luminosidad, bool enviarData, string nombrePlaca);
    void RecieveMovimientoData(bool movement, string nombrePlaca);
}
