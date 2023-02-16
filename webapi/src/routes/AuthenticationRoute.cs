namespace wabapi.routes;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webapi.interfaces;


public class AuthenticationRoute : IRoute
{
    public IConfiguration configuration { get; set; }

    public AuthenticationRoute (IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private string GetToken()
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("Name", "John Doe"));
        claims.Add(new Claim("Email", "John@doe.com"));
        claims.Add(new Claim("Role", "Admin"));

        string issuer = configuration["Jwt:Issuer"] ?? "NA";
        string audience = configuration["Jwt:Audience"] ?? "NA";
        string signingKey = configuration["Jwt:SigningKey"] ?? "NA";
        string expirationMinutes = configuration["Jwt:ExpirationMinutes"] ?? "0";

        string token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(expirationMinutes)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)), SecurityAlgorithms.HmacSha256Signature)
        ));
        
        return token;
    }

    public void Register(WebApplication app)
    {
        app.MapGet("/api/auth", () => GetToken());
    }
}
