using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.UI.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private const string AuthCookieName = "CentroEventosAuth";
        private ClaimsPrincipal _anonymous => new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var (userId, userName) = await GetUserNameFromCookieAsync();
                if (!string.IsNullOrEmpty(userName))
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                    }, AuthCookieName);
                    var user = new ClaimsPrincipal(identity);
                    return new AuthenticationState(user);
                }
            }
            catch (InvalidOperationException)
            {
                // Si ocurre una excepción, retornamos un estado anónimo
                Console.WriteLine("Error al obtener el nombre de usuario del cookie.");
            }
            return new AuthenticationState(_anonymous);
        }

        public async Task MarkUserAsAuthenticated(int userId, string userName)
        {
            if (userId <= 0)
                throw new ValidacionException("El userId debe ser mayor a cero.");

            if (string.IsNullOrEmpty(userName))
                throw new ValidacionException("El userName no puede ser nulo o vacío.");

            await SetAuthCookieAsync(userId, userName, 2); // 2 horas
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }, AuthCookieName);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await RemoveAuthCookieAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private async Task<(string userId, string userName)> GetUserNameFromCookieAsync()
        {
            return await _jsRuntime.InvokeAsync<(string userId, string userName)>($"{AuthCookieName}.getAuthUser");
        }

        private async Task SetAuthCookieAsync(int userId, string userName, int hours)
        {
            await _jsRuntime.InvokeVoidAsync($"{AuthCookieName}.setAuthUser", userId, userName, hours);
        }

        private async Task RemoveAuthCookieAsync()
        {
            await _jsRuntime.InvokeVoidAsync($"{AuthCookieName}.removeAuthUser");
        }
    }
}
