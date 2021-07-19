using JwtUserDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserDataAccess
{
    public class JwtUserDbContext : DbContext
    {
        public JwtUserDbContext(DbContextOptions<JwtUserDbContext> opts):base(opts)
        {
            DbContextOptionsBuilder<JwtUserDbContext> optionsBuilder = new DbContextOptionsBuilder<JwtUserDbContext>().UseNpgsql("Host = localhost; Port = 5432; Database = jwt_users; User ID = car_usr; Password = samochod");
        }

        public DbSet<JwtUserModel> JwtUsers { get; set; }
    }
}
