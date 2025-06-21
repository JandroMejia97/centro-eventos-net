using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CentroEventos.UI.Auth;

public static class AuthConstant
{
    public const string AuthCookieName = "CentroEventosAuth";
}

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (_httpContextAccessor.HttpContext!.Request.Cookies.ContainsKey(AuthConstant.AuthCookieName))
            {
                var cookie = _httpContextAccessor.HttpContext.Request.Cookies[AuthConstant.AuthCookieName];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(cookie) as JwtSecurityToken;
                var claims = new List<Claim>();
                foreach (var claim in jsonToken!.Claims)
                {
                    claims.Add(new Claim(claim.Type, claim.Value));
                }

                var claimsIdentity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(claimsIdentity);
                return Task.FromResult(new AuthenticationState(user));
            }
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
        catch (Exception)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
    }

    public string MarkUserAsAuthenticated(int userId, string userName, Persona persona, IEnumerable<Permiso> permisos)
    {
        DateTime expiresAt = DateTime.UtcNow.AddHours(2);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, userName),
            new(ClaimTypes.Expiration, expiresAt.ToString("o")),
            new(ClaimTypes.Name, persona.NombreCompleto),
            new(ClaimTypes.UserData, JsonSerializer.Serialize(new
            {
                Persona = new
                {
                    persona.Id,
                    persona.DNI,
                    persona.Nombre,
                    persona.Apellido,
                    persona.Email,
                    persona.Telefono
                },
                Permisos = permisos.ToArray()
            }))
        };

        var claimsIdentity = new ClaimsIdentity(claims, "apiauth");
        var token = new JwtSecurityToken(
            issuer: "https://centroeventos.com",
            audience: Guid.NewGuid().ToString(),
            claims: claimsIdentity.Claims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                SecurityAlgorithms.HmacSha256)
        );
        var user = new ClaimsPrincipal(claimsIdentity);

        var authState = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public int ExtractUserIdAsync()
    {
        var Token = _httpContextAccessor.HttpContext!.Request.Cookies[AuthConstant.AuthCookieName];
        if (string.IsNullOrEmpty(Token))
        {
            return 0; // No user is authenticated
        }
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(Token) as JwtSecurityToken;
        if (jsonToken is null)
        {
            return 0; // Invalid token
        }
        var user = new ClaimsPrincipal(new ClaimsIdentity(jsonToken.Claims, "jwt"));
        if (user.Identity is null)
        {
            return 0; // No user identity found
        }

        Console.WriteLine($"User: {user.Identity?.Name}, Authenticated: {user.Identity?.IsAuthenticated}");
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
        }
        return 0;
    }

    public void MarkUserAsLoggedOutAsync()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        _httpContextAccessor.HttpContext!.Response.Cookies.Delete(AuthConstant.AuthCookieName);
    }
}
