using FakeFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Data
{
    public class TeamRepo : ITeamRepo
    {
        private readonly SoccerDbContext _context;

        public TeamRepo(SoccerDbContext context)
        {
            _context = context;
        }

        public void CreateTeam(TeamModel team)
        {
            _context.Team.Add(team);
        }

        public void DeleteTeam(TeamModel team)
        {
            _context.Team.Remove(team);
        }

        public List<TeamModel> GetAllTeams()
        {
            var res = _context.Team.OrderByDescending(t=>t.Points).ToList();

            return (res);
        }

        public TeamModel GetOneTeam(int id)
        {
            var res = _context.Team.FirstOrDefault(t=>t.Id==id);

            return (res);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateTeam(TeamModel team)
        {
            //do nothing automapper does
        }
    }
}
