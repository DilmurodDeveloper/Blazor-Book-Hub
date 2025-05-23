using System.ComponentModel.DataAnnotations;

namespace BlazorBookHub.Shared.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title must be less than 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name must be less than 100 characters")]
        public string Author { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters")]
        public string Description { get; set; } = string.Empty;

        public string CoverImagePath { get; set; } = string.Empty;

        public string PdfPath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
