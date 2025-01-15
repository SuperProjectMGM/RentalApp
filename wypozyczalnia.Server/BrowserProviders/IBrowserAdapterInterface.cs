using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.BrowserProviders;

public interface IBrowserAdapterInterface
{
    public Task RentalCompleted(Rental rental);

    public Task AcceptReturn(Rental rental, Vehicle vehicle);
}