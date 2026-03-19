using System.Collections.Generic;
using VolleyballManager.Data.Models;

namespace VolleyballManager.Services
{
    public interface IMatchService
    {
        void AddMatch(Match match);
        IEnumerable<Match> GetAllMatches();
        Match GetById(int id);
        void UpdateMatch(Match match);
        void DeleteMatch(int id);
    }
}