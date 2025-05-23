using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(int id, CreateCategoryDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
