namespace BlazorBookHub.Shared.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
