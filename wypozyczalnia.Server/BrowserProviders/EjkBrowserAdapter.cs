using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.BrowserProviders;

public class EjkBrowserAdapter : IBrowserAdapterInterface
{
    private readonly EjkBrowser _adaptee;


    public EjkBrowserAdapter(HttpClient httpClient)
    {
        _adaptee = new EjkBrowser(httpClient);
    }
    
    public async Task RentalCompleted(Rental rental)
    {
        await _adaptee.RentalCompleted(rental);
    }

    public async Task AcceptReturn(Rental rental)
    {
        await _adaptee.AcceptReturn(rental);
    }
}