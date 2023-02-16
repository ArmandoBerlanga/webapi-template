namespace webapi.config;

using wabapi.routes;
using webapi.interfaces;


public static class Routes
{
    public static IServiceCollection ConfigRoutes(this IServiceCollection services)
    {
        services.AddTransient<IRoute, InformationRoute>();
        services.AddTransient<IRoute, AuthenticationRoute>();
        return services;
    }

    public static WebApplication AddRoutes(this WebApplication app)
    {
        IEnumerable<IRoute> routes = app.Services.GetServices<IRoute>();
        foreach (IRoute route in routes)        
            route.Register(app);

        return app;
    }    
}
