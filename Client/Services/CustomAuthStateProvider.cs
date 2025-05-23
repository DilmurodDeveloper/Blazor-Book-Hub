using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorBookHub.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            ClaimsIdentity identity;

            if (string.IsNullOrWhiteSpace(token))
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                if (IsTokenValid(token))
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                else
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims;
        }

        private bool IsTokenValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
