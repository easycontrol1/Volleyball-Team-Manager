using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VolleyballManager.Data;
using VolleyballManager.Data.Models;
using VolleyballManager.Data.Models;
using VolleyballManager.Data;

namespace VolleyballManager.Services 
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext context;

     
        public PlayerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddPlayer(Player player)
        {
            context.Players.Add(player);
            context.SaveChanges();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return context.Players.OrderBy(p => p.LastName).ToList();
        }

        public Player GetById(int id)
        {
            return context.Players.FirstOrDefault(p => p.Id == id);
        }
    }
}