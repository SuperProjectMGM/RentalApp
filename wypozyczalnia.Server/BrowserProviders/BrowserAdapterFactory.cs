using wypozyczalnia.Server.Services;

namespace wypozyczalnia.Server.BrowserProviders;

public class BrowserAdapterFactory
{
    public static IBrowserAdapterInterface CreateBrowser(string identifier, RabbitMessageService rabbit, HttpClient httpClient)
    {
        switch (identifier)
        {
            case "MGMCO":
                return new MgmBrowserAdapter(rabbit);
            case "EJKCO":
                return new EjkBrowserAdapter(httpClient);
            default:
                throw new KeyNotFoundException("Unknown browser.");
        }
    }
}