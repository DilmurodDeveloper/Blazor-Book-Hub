using Microsoft.AspNetCore.Components.Forms;

namespace BlazorBookHub.Client.Models
{
    public class BookUploadClientDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
