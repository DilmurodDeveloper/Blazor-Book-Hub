using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorBookHub.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(
            HttpClient http, 
            ILocalStorageService localStorage, 
            AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(string email, string password)
        {
            var loginModel = new { Email = email, Password = password };
            var response = await _http.PostAsJsonAsync("api/auth/login", loginModel);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                
                ((JwtAuthenticationStateProvider)_authStateProvider)
                    .NotifyUserAuthentication(result.Token);
                
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.Token);

                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
            _http.DefaultRequestHeaders.Authorization = null;
        }

        private class LoginResult
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
