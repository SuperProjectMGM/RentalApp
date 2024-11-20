using wypozyczalnia.Server.Services;
namespace wypozyczalnia.Server.AppExtensions;

public static class ApplicationBuilderExtensions
{
    private static RabbitListener? _listener;

    public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
    {
        _listener = app.ApplicationServices.GetService<RabbitListener>();

        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime?.ApplicationStarted.Register(OnStarted);

        lifetime?.ApplicationStopping.Register(OnStopping);

        return app;
    }

    private static void OnStarted()
    {
        _listener?.Register();
    }

    private static void OnStopping()
    {
        _listener?.Deregister();
    }
}