using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using AutoMapper;

namespace BlazorBookHub.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => _mapper.Map<CategoryDto>(c));
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            var created = await _repository.AddAsync(category);
            return _mapper.Map<CategoryDto>(created);
        }

        public async Task UpdateAsync(int id, CreateCategoryDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Category with id {id} not found.");

            existing.Name = dto.Name;
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with id {id} not found.");

            await _repository.DeleteAsync(category);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
