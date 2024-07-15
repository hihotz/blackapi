using blackapi.Models;
using Microsoft.EntityFrameworkCore;

namespace blackapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // 마이그레이션
        // dotnet ef migrations add ProductionTableCreate
        // dotnet ef database update
        public DbSet<Production> Productions { get; set; }
    }
}