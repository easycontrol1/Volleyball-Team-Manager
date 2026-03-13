using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolleyballManager.Data.Models; // <-- Внимание! Провери дали namespace-а ти е точно такъв (с долни черти)

namespace VolleyballManager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        // ДОБАВИ ТЕЗИ РЕДОВЕ:
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    }
}