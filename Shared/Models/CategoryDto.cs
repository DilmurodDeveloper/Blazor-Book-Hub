using System.ComponentModel.DataAnnotations;

namespace BlazorBookHub.Shared.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
