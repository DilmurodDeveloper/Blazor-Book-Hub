using BlazorBookHub.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
