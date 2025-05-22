using AutoMapper;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Services
{
    public class BookService(IBookRepository repo, IMapper mapper) : IBookService
    {
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await repo.GetAllAsync();
            return books.Select(b => mapper.Map<BookDto>(b));
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await repo.GetByIdAsync(id);
            return book is null ? null : mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
            };
            var created = await repo.AddAsync(book);
            return mapper.Map<BookDto>(created);
        }

        public async Task UpdateAsync(int id, CreateBookDto dto)
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) throw new Exception("Book not found");

            existing.Title = dto.Title;
            existing.Author = dto.Author;
            existing.Description = dto.Description;
            existing.CategoryId = dto.CategoryId;

            await repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await repo.GetByIdAsync(id);
            if (book is null) throw new Exception("Book not found");
            await repo.DeleteAsync(book);
        }

        public async Task AddAsync(Book book)
        {
            await repo.AddAsync(book);
        }
    }
}
