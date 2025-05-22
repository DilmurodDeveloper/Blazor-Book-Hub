using Microsoft.AspNetCore.Identity;

namespace BlazorBookHub.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }          
        public DateTime? BirthDate { get; set; }        
        public string? Gender { get; set; }            
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public bool IsActive { get; set; } = true;    
    }
}
