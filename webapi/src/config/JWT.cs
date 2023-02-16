namespace webapi.config;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


public static class JWT
{
    public static IServiceCollection ConfigJWTParameters(this IServiceCollection services)
    {
        IConfiguration? configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        string issuer = configuration?["Jwt:Issuer"] ?? "NA";
        string audience = configuration?["Jwt:Audience"] ?? "NA";
        string signingKey = configuration?["Jwt:SigningKey"] ?? "NA";

        TokenValidationParameters parameters = new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
        };

        services.AddSingleton(() => parameters);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = parameters);

        return services;
    }
}
