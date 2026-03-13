using System.Collections.Generic;
using VolleyballManager.Data.Models; // Този namespace трябва да ти е правилен

namespace VolleyballManager.Services
{
    public interface IPlayerService
    {
        void AddPlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetById(int id);
    }
}