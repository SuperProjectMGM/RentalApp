namespace wypozyczalnia.Server.Messages;

public class EmployeeReturn
{
    public string Slug { get; set; } = string.Empty;
    public float PaymentDue { get; set; }
}