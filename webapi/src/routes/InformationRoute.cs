namespace wabapi.routes;

using webapi.interfaces;


public class InformationRoute : IRoute
{
    private IDapperRepository _connection; 

    public InformationRoute(IDapperRepository connection)
    {
        _connection = connection;
    }

    async private Task<Object> Echo()
    {
        string response = await _connection.QueryFirstAsync<string>("SELECT GETDATE() AS [Response]");
        
        return new
        {
            Message = response
        };
    }

    private Object GetWebApiInfo()
    {     
        return new
        {
            Name = "PPLUS API v2",
            EndPointsRoute = "https://localhost:5001/api"
        };
    }

    public void Register(WebApplication app)
    {
        app.MapGet("/api/echo", () => Echo());
        app.MapGet("/api/info", () => GetWebApiInfo())
            .RequireAuthorization();
    }
}
