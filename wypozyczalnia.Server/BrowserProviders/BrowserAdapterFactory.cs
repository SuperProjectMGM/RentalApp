using wypozyczalnia.Server.Services;

namespace wypozyczalnia.Server.BrowserProviders;

public class BrowserAdapterFactory
{
    public static IBrowserAdapterInterface CreateBrowser(string identifier, RabbitMessageService rabbit)
    {
        switch (identifier)
        {
            case "MGMCO":
                return new MgmBrowserAdapter(rabbit);
            default:
                throw new KeyNotFoundException("Unknown browser.");
        }
    }
}