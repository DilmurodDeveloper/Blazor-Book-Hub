using AutoMapper;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Services
{
    public class CategoryService(ICategoryRepository repo, IMapper mapper) : ICategoryService
    {
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await repo.GetAllAsync();
            return categories.Select(mapper.Map<CategoryDto>);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);
            return category is null ? null : mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            var created = await repo.AddAsync(category);
            return mapper.Map<CategoryDto>(created);
        }

        public async Task UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await repo.GetByIdAsync(id);
            if (category is null) throw new Exception("Category not found");

            category.Name = dto.Name;
            await repo.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);
            if (category is null) throw new Exception("Category not found");

            await repo.DeleteAsync(category);
        }
    }
}
