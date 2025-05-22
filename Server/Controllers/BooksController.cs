using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorBookHub.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IFileStorageService _fileStorageService;

        public BooksController(IBookService bookService, IFileStorageService fileStorageService)
        {
            _bookService = bookService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var created = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookDto dto)
        {
            await _bookService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromForm] BookUploadDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pdfPath = await _fileStorageService.SaveFileAsync(model.PdfFile!, "pdfs");
            var imgPath = await _fileStorageService.SaveFileAsync(model.ImageFile!, "images");

            var newBook = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description ?? string.Empty,
                CategoryId = model.CategoryId,
                PdfPath = pdfPath,
                CoverImagePath = imgPath,
                CreatedAt = DateTime.UtcNow
            };

            await _bookService.AddAsync(newBook);

            return Ok(new { message = "Kitob yuklandi", book = newBook });
        }
    }
}
