using BlazorBookHub.Server.Data;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() =>
            await _context.Books.Include(b => b.Category).ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) =>
            await _context.Books.Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
