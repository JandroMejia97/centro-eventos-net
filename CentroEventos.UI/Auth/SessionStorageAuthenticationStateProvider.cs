using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace CentroEventos.UI.Auth
{
    public class SessionStorageAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private const string SessionKey = "authState";
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public SessionStorageAuthenticationStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_currentUser.Identity != null && _currentUser.Identity.IsAuthenticated)
            {
                return new AuthenticationState(_currentUser);
            }
            if (_js is not IJSInProcessRuntime)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            try
            {
                var json = await _js.InvokeAsync<string>("sessionStorage.getItem", SessionKey);
                if (!string.IsNullOrEmpty(json))
                {
                    var state = JsonSerializer.Deserialize<AuthStateDto>(json);
                    if (state != null && state.IsAuthenticated)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, state.UserId),
                            new Claim(ClaimTypes.Name, state.Email)
                        }, "SessionStorage");
                        _currentUser = new ClaimsPrincipal(identity);
                    }
                }
            }
            catch
            {
                // Si falla JS interop, devolver usuario no autenticado
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            return new AuthenticationState(_currentUser);
        }

        public async Task MarkUserAsAuthenticated(string userId, string email)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email)
            }, "SessionStorage");
            _currentUser = new ClaimsPrincipal(identity);
            var state = new AuthStateDto { IsAuthenticated = true, UserId = userId, Email = email };
            await _js.InvokeVoidAsync("sessionStorage.setItem", SessionKey, JsonSerializer.Serialize(state));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await _js.InvokeVoidAsync("sessionStorage.removeItem", SessionKey);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private class AuthStateDto
        {
            public bool IsAuthenticated { get; set; }
            public string UserId { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
    }
}
