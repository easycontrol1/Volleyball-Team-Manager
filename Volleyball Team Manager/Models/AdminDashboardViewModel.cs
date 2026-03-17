using VolleyballManager.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace VolleyballManager.Models
{
    public class AdminDashboardViewModel
    {
        // Статистика
        public int TotalPlayers { get; set; }
        public int TotalMatches { get; set; }
        public int TotalUsers { get; set; }
        public string LastMatchOpponent { get; set; }

        // Списък с потребители
        public IList<IdentityUser> Users { get; set; }
    }
}