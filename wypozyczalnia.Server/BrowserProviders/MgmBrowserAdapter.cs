using Microsoft.CodeAnalysis.Elfie.Serialization;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;

namespace wypozyczalnia.Server.BrowserProviders;

public class MgmBrowserAdapter : IBrowserAdapterInterface
{

    private readonly MgmBrowser _adaptee;
    
    public MgmBrowserAdapter(RabbitMessageService rabbit)
    {
        _adaptee = new MgmBrowser(rabbit);
    }
    
    public async Task RentalCompleted(Rental rental)
    {
        await _adaptee.RentalCompleted(rental);
    }

    public async Task AcceptReturn(Rental rental, Vehicle vehicle)
    {
        await _adaptee.AcceptReturn(rental, vehicle);
    }
}