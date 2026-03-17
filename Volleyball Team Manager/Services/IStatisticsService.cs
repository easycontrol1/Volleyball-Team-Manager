using System.Collections.Generic;
using VolleyballManager.Data.Models;

namespace VolleyballManager.Services
{
    public interface IStatisticsService
    {
        void AddStatistic(PlayerStatistic statistic);
        List<PlayerStatistic> GetStatisticsByMatch(int matchId);
    }
}