using BlazorBookHub.Server.Data;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await context.Categories.FindAsync(id);

        public async Task<Category> AddAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
