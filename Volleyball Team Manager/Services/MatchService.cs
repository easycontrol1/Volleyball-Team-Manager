using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VolleyballManager.Data;
using VolleyballManager.Data.Models;

namespace VolleyballManager.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext context;

        public MatchService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddMatch(Match match)
        {
            context.Matches.Add(match);
            context.SaveChanges();
        }

        public IEnumerable<Match> GetAllMatches()
        {
            return context.Matches.OrderByDescending(m => m.Date).ToList();
        }

        public Match GetById(int id)
        {
            return context.Matches.FirstOrDefault(m => m.Id == id);
        }

        // --- НОВИ МЕТОДИ ---

        public void UpdateMatch(Match match)
        {
            var dbMatch = context.Matches.FirstOrDefault(m => m.Id == match.Id);
            if (dbMatch != null)
            {
                dbMatch.Date = match.Date;
                dbMatch.Opponent = match.Opponent;
                dbMatch.Result = match.Result;
                dbMatch.IsHomeGame = match.IsHomeGame;
                context.SaveChanges();
            }
        }

        public void DeleteMatch(int id)
        {
            var match = context.Matches.FirstOrDefault(m => m.Id == id);
            if (match != null)
            {
                context.Matches.Remove(match);
                context.SaveChanges();
            }
        }
    }
}