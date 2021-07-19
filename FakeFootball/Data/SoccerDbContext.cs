using FakeFootball.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Data
{
    public class SoccerDbContext : DbContext
    {
        public SoccerDbContext(DbContextOptions<SoccerDbContext> opts):base(opts)
        {

        }

        public DbSet<ScoresModel> Scores { get; set; }

        public DbSet<TeamModel> Team { get; set; }
    }
}
