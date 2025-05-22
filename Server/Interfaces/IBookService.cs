using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task UpdateAsync(int id, CreateBookDto dto);
        Task DeleteAsync(int id);
    }
}
