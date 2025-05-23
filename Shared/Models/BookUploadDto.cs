using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlazorBookHub.Shared.Models
{
    public class BookUploadDto
    {
        [Required] 
        public string Title { get; set; } = string.Empty;
        
        [Required] 
        public string Author { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        [Required] 
        public int CategoryId { get; set; }

        [Required] 
        public IFormFile? PdfFile { get; set; }
        
        [Required] 
        public IFormFile? ImageFile { get; set; }
    }
}
