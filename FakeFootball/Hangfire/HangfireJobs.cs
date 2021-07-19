using FakeFootball.Data;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFootball.Hangfire
{
    public class HangfireJobs : IHangfireJobs
    {
        private readonly IScoresRepo _scoresRepo;
        private readonly IConfiguration _config;

        public HangfireJobs(IScoresRepo scoresRepo,IConfiguration config)
        {
            _scoresRepo = scoresRepo;
            _config = config;
        }

        public void GenerateKey()
        {
            Random rnd = new Random();
            StringBuilder st = new StringBuilder();
            for(int i = 0; i < rnd.Next(15, 20); i++)
            {
                st.Append(Convert.ToChar(rnd.Next(97,122)));
            }

            _config["Jwt:Key"] = st.ToString();
        }

        public void UpdateKey()
        {
            RecurringJob.AddOrUpdate("Job3", () => GenerateKey(), Cron.Hourly);
        }

        public void UpdateScores()
        {
            RecurringJob.AddOrUpdate("Job2",()=>_scoresRepo.UpdateScores(),Cron.Daily);
        }
    }
}
