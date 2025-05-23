using System.ComponentModel.DataAnnotations;

namespace BlazorBookHub.Shared.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
    }
}
