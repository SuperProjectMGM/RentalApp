namespace wypozyczalnia.Server.Interfaces;

public interface IMessageHandler
{
    public Task ProcessMessage(string message);
}