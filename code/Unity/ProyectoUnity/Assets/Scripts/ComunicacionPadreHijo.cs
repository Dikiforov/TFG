public interface ISensorDataReciever
{
    void RecieveTempData(float temp);
    void RecieveDoorState(bool isOpen, string doorName);
}
