using Microsoft.EntityFrameworkCore;     // ← ОБЯЗАТЕЛЬНО
using ServiceDesk.Api.Models;

namespace ServiceDesk.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
    }
}
