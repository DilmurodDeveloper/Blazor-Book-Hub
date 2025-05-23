using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        if (book == null)
            return NotFound(new { message = $"Book with id {id} not found." });

        return Ok(book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _bookService.ExistsAsync(id);
        if (!exists)
            return NotFound(new { message = $"Book with id {id} not found." });

        await _bookService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exists = await _bookService.ExistsAsync(id);
        if (!exists)
            return NotFound(new { message = $"Book with id {id} not found." });

        await _bookService.DeleteAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadBook([FromForm] BookUploadDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var pdfPath = await _fileStorageService.SaveFileAsync(model.PdfFile!, "pdfs");
            var imgPath = await _fileStorageService.SaveFileAsync(model.ImageFile!, "images");

            var dto = new CreateBookDto
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description ?? string.Empty,
                CategoryId = model.CategoryId,
                PdfPath = pdfPath,
                CoverImagePath = imgPath
            };

            var createdBook = await _bookService.CreateAsync(dto);

            return Ok(new { message = "Book successfully uploaded", book = createdBook });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during the file upload process.", error = ex.Message });
        }
    }
}
