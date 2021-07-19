using FakeFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeFootball.Data
{
    public class ScoresRepo : IScoresRepo
    {
        private readonly SoccerDbContext _context;

        public ScoresRepo(SoccerDbContext context)
        {
            _context = context;
        }

        public List<ScoresModel> GetAllScores()
        {
            var res = _context.Scores.OrderByDescending(s => s.GamePlayedAt).Take(20).ToList();

            return (res);
        }

        public List<ScoresModel> GetAllScoresAfter(DateTime date)
        {
            var res = _context.Scores.Where(s => s.GamePlayedAt > date).ToList();

            return (res);
        }

        public List<ScoresModel> GetAllTeamScores(string teamName)
        {
            var res = _context.Scores.Where(s => s.HomeTeam == teamName || s.AwayTeam == teamName).ToList();

            return (res);
        }

        public ScoresModel GetOneScores(int id)
        {
            var res = _context.Scores.FirstOrDefault(s => s.Id == id);

            return (res);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private void UpdateTeamPoints(TeamModel team,int res)
        {
            var fromDB = _context.Team.FirstOrDefault(t => t.Id == team.Id);
            fromDB.Points += res;
            fromDB.GamesPlayed++;
            _context.SaveChanges();
        }

        private void AffectTeams(List<TeamModel> teams)
        {
            Random rnd = new Random();

            while (teams.Count > 0)
            {
                var homeTeam = teams[rnd.Next(0, teams.Count)];
                teams.Remove(homeTeam);

                var awayTeam = teams[rnd.Next(0, teams.Count)];
                teams.Remove(awayTeam);

                int homeScore = rnd.Next(0, 5);
                int awayScore = rnd.Next(0, 5);
                ScoresModel sm = new ScoresModel()
                {
                    HomeTeam = homeTeam.TeamName,
                    AwayTeam = awayTeam.TeamName,
                    HomeScore = homeScore,
                    AwayScore = awayScore
                };

                if (homeScore > awayScore)
                {
                    UpdateTeamPoints(homeTeam,3);
                    UpdateTeamPoints(awayTeam, 0);

                }
                else if (homeScore == awayScore)
                {
                    UpdateTeamPoints(homeTeam, 1);
                    UpdateTeamPoints(awayTeam, 1);
                }
                else
                {
                    UpdateTeamPoints(homeTeam, 0);
                    UpdateTeamPoints(awayTeam, 3);
                }

                _context.Scores.Add(sm);
                this.SaveChanges();
            }
        }

        public bool UpdateScores()
        {
            var teams = _context.Team.ToList();

            AffectTeams(teams);

            return true;
        }
    }
}
