using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VolleyballManager.Data; // Тук сложи твоя namespace (провери ApplicationDbContext-а)
using VolleyballManager.Data.Models;

namespace VolleyballManager.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext context;

        public StatisticsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddStatistic(PlayerStatistic statistic)
        {
            statistic.Player = null;

            var existing = context.PlayerStatistics
                .FirstOrDefault(x => x.MatchId == statistic.MatchId && x.PlayerId == statistic.PlayerId);

            if (existing != null)
            {
                existing.ServicePoints = statistic.ServicePoints;
                existing.AttackPoints = statistic.AttackPoints;
                existing.BlockPoints = statistic.BlockPoints;
                existing.PositiveReceptions = statistic.PositiveReceptions;
                existing.Errors = statistic.Errors;
            }
            else
            {
                context.PlayerStatistics.Add(statistic);
            }

            context.SaveChanges();
        }

        public List<PlayerStatistic> GetStatisticsByMatch(int matchId)
        {
            
            return context.PlayerStatistics
                .Include(x => x.Player)
                .Where(x => x.MatchId == matchId)
                .ToList();
        }

    }
}