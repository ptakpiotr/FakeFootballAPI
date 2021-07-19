using FakeFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Data
{
    public interface IScoresRepo
    {
        List<ScoresModel> GetAllScores();
        ScoresModel GetOneScores(int id);
        List<ScoresModel> GetAllScoresAfter(DateTime date);
        List<ScoresModel> GetAllTeamScores(string teamName);

        bool UpdateScores();
        void SaveChanges();
    }
}
