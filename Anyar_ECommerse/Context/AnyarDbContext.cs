using Anyar_ECommerse.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Anyar_ECommerse.Context
{
    public class AnyarDbContext:IdentityDbContext
    {
        public AnyarDbContext(DbContextOptions<AnyarDbContext> options) : base(options){}

        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
