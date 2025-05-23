using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorBookHub.Client.Services
{
    public class AuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authProvider;
        private readonly NavigationManager _navigation;

        public AuthService(ILocalStorageService localStorage, AuthenticationStateProvider authProvider, NavigationManager navigation)
        {
            _localStorage = localStorage;
            _authProvider = authProvider;
            _navigation = navigation;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            if (_authProvider is CustomAuthStateProvider customAuth)
            {
                customAuth.NotifyUserLogout();
            }
            _navigation.NavigateTo("/login");
        }
    }
}
