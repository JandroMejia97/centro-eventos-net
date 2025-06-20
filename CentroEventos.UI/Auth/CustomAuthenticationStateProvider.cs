using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;

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
            var userName = await GetUserNameFromCookieAsync();
            if (string.IsNullOrEmpty(userName))
                return new AuthenticationState(_anonymous);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName)
            }, AuthCookieName);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string userName)
        {
            await SetAuthCookieAsync(userName, 2); // 2 horas
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName)
            }, AuthCookieName);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await RemoveAuthCookieAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private async Task<string> GetUserNameFromCookieAsync()
        {
            return await _jsRuntime.InvokeAsync<string>($"{AuthCookieName}.getAuthUser");
        }

        private async Task SetAuthCookieAsync(string userName, int hours)
        {
            await _jsRuntime.InvokeVoidAsync($"{AuthCookieName}.setAuthUser", userName, hours);
        }

        private async Task RemoveAuthCookieAsync()
        {
            await _jsRuntime.InvokeVoidAsync($"{AuthCookieName}.removeAuthUser");
        }
    }
}
