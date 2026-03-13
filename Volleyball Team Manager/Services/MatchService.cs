using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VolleyballManager.Data.Models;
using VolleyballManager.Data;

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
            // Подреждаме мачовете по дата (най-новите най-отгоре)
            return context.Matches.OrderByDescending(m => m.Date).ToList();
        }

        public Match GetById(int id)
        {
            return context.Matches.FirstOrDefault(m => m.Id == id);
        }
    }
}