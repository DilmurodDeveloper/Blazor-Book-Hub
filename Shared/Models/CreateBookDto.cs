namespace BlazorBookHub.Shared.Models
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? PdfPath { get; set; }
        public string? CoverImagePath { get; set; }
    }
}