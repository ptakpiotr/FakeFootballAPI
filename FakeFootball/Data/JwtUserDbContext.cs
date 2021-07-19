using Microsoft.EntityFrameworkCore;
using FakeFootball.Models;

namespace FakeFootball.Data
{
    public class JwtUserDbContext : DbContext
    {
        public JwtUserDbContext(DbContextOptions<JwtUserDbContext> opts) : base(opts)
        {

        }

        public DbSet<JwtUserModel> JwtUsers { get; set; }
    }
}
