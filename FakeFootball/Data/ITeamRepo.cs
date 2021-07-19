using FakeFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Data
{
    public interface ITeamRepo
    {
        List<TeamModel> GetAllTeams();
        TeamModel GetOneTeam(int id);
        void CreateTeam(TeamModel team);

        void UpdateTeam(TeamModel team);
        void DeleteTeam(TeamModel team);

        void SaveChanges();
    }
}
