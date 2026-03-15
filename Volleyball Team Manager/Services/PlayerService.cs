using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VolleyballManager.Data;
using VolleyballManager.Data.Models;

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

        public void DeletePlayer(int id)
        {
            var player = context.Players.FirstOrDefault(p => p.Id == id);
            if (player != null)
            {
                context.Players.Remove(player);
                context.SaveChanges();
            }
        }
        public void UpdatePlayer(Player player)
        {
            var dbPlayer = context.Players.FirstOrDefault(p => p.Id == player.Id);
            if (dbPlayer != null)
            {
                dbPlayer.FirstName = player.FirstName;
                dbPlayer.LastName = player.LastName;
                dbPlayer.ShirtNumber = player.ShirtNumber;
                dbPlayer.Position = player.Position;
                dbPlayer.Height = player.Height;
                dbPlayer.WingSpan = player.WingSpan;
                dbPlayer.VerticalJump = player.VerticalJump;
                dbPlayer.Age = player.Age;

                context.SaveChanges();
            }
        }
    }
}