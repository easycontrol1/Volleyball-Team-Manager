using VolleyballManager.Data.Models;

namespace VolleyballManager.Models
{
    public class MatchStatisticsViewModel
    {
        public int MatchId { get; set; }
        public string? OpponentName { get; set; }
        public List<PlayerStatistic> PlayerStats { get; set; } = new List<PlayerStatistic>();
    }
}