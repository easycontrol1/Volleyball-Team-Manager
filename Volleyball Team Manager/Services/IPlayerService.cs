using System.Collections.Generic;
using VolleyballManager.Data.Models;

namespace VolleyballManager.Services
{
    public interface IPlayerService
    {
        void AddPlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetById(int id);
        void UpdatePlayer(Player player); // Това го имаш от редакцията
        void DeletePlayer(int id);        // <-- ДОБАВИ ТОЗИ РЕД
    }
}