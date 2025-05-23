using AutoMapper;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _repo.GetAllAsync();
            return books.Select(b => _mapper.Map<BookDto>(b));
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description ?? string.Empty,
                CategoryId = dto.CategoryId,
                PdfPath = dto.PdfPath ?? string.Empty,
                CoverImagePath = dto.CoverImagePath ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(book);
            return _mapper.Map<BookDto>(created);
        }

        public async Task UpdateAsync(int id, CreateBookDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Book with id {id} not found.");

            existing.Title = dto.Title;
            existing.Author = dto.Author;
            existing.Description = dto.Description ?? string.Empty;
            existing.CategoryId = dto.CategoryId;

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with id {id} not found.");

            await _repo.DeleteAsync(book);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}
