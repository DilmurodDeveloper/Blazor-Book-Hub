namespace BlazorBookHub.Shared.Models
{
    public class UpdateUserProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
    }
}
