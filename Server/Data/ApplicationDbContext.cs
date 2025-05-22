using BlazorBookHub.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
