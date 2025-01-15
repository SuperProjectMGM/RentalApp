using wypozyczalnia.Server.Messages;

namespace wypozyczalnia.Server.Interfaces;

public interface IMessageHandlerInterface
{
    public Task ProcessMessage(MessageMgm decisionMsg, string serializedMsg);
}