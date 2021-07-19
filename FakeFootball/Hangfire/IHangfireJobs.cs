namespace FakeFootball.Hangfire
{
    public interface IHangfireJobs
    {
        void UpdateScores();
        void UpdateKey();
    }
}