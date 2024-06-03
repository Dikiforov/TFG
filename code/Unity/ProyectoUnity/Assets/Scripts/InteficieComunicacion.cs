using System;

public interface ISensorDataReciever
{
    void RecieveTempData(float temp, bool enviarData, string nombrePlaca, DateTime fecha);
    void RecieveDoorState(bool isOpen, string doorName, string nombrePlaca, DateTime fecha);
    void RecieveHumedadData(float humedad, bool enviarData, string nombrePlaca, DateTime fecha);
    void RecieveLuminosidadData(float luminosidad, bool enviarData, string nombrePlaca, DateTime fecha);
    void RecieveMovimientoData(bool movement, string nombrePlaca, DateTime fecha);
}
