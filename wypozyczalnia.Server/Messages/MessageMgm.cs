using System.Text.Json.Serialization;
using wypozyczalnia.Server.DTOs;

namespace wypozyczalnia.Server.Messages;

[JsonDerivedType(typeof(MessageMgmConfirmed), typeDiscriminator: "MessageMgmConfirmed")]
[JsonDerivedType(typeof(MessageMgmCompleted), typeDiscriminator: "MessageMgmCompleted")]
public class MessageMgm
{
    public MessageType MessageType;
}
public enum MessageType
{
    UserConfirmedRental = 0,
    EmployeeCompletedRental = 1
}
