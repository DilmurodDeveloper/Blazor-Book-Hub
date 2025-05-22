using BlazorBookHub.Server.Data;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Repositories
{
    public class BookRepository(ApplicationDbContext context) : IBookRepository
    {
        public async Task<IEnumerable<Book>> GetAllAsync() =>
            await context.Books.Include(b => b.Category).ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) =>
            await context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Book> AddAsync(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}
