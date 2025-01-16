using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Messages;

namespace wypozyczalnia.Server.Interfaces;

public interface IMessageHandlerInterface
{
    public Task ProcessMessage(string serializedMsg);

    public Task ProcessExternalConfirm(ConfirmedExternal externalMessage);

    public Task ProcessExternalReturn(UserReturnExternal externalMessage);

}