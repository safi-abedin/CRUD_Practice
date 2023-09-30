using FullStack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Data
{
    public class FullstackDbContext : DbContext
    {
        public FullstackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } 

    }
}
