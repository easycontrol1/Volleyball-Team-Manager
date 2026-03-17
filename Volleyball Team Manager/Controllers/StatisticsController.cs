using Microsoft.AspNetCore.Mvc;
using VolleyballManager.Data.Models;
using VolleyballManager.Models;
using VolleyballManager.Services;

namespace VolleyballManager.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IMatchService matchService;
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IPlayerService pService, IMatchService mService, IStatisticsService sService)
        {
            playerService = pService;
            matchService = mService;
            statisticsService = sService;
        }

        // 1. ПОКАЗВА ФОРМАТА (GET)
        public IActionResult Index(int matchId)
        {
            var match = matchService.GetById(matchId);
            if (match == null) return NotFound();

            var viewModel = new MatchStatisticsViewModel
            {
                MatchId = match.Id,
                OpponentName = match.Opponent
            };

            
            var allPlayers = playerService.GetAllPlayers();
            
            var existingStats = statisticsService.GetStatisticsByMatch(matchId);

            
            viewModel.PlayerStats = allPlayers.Select(player =>
            {
                var stat = existingStats.FirstOrDefault(s => s.PlayerId == player.Id);

                return stat ?? new PlayerStatistic
                {
                    PlayerId = player.Id,
                    MatchId = match.Id,
                    Player= player,
                    ServicePoints = 0,
                    AttackPoints = 0,
                    BlockPoints = 0,
                    PositiveReceptions = 0,
                    Errors = 0
                };
            }).ToList();

            return View(viewModel);
        }

        // 2. ЗАПИСВАНЕ НА ДАННИТЕ (POST)
        [HttpPost]
        public IActionResult Save(MatchStatisticsViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var stat in model.PlayerStats)
                {
                    statisticsService.AddStatistic(stat);
                }
                return RedirectToAction("Details", new { matchId = model.MatchId });
            }

            return View("Index", model);
        }
        // 3. ПРОГЛЕД НА СТАТИСТИКАТА (Details)
        
        public IActionResult Details(int matchId)
        {
            var match = matchService.GetById(matchId);
            if (match == null) return NotFound();

            
            var allPlayers = playerService.GetAllPlayers();
            
            var existingStats = statisticsService.GetStatisticsByMatch(matchId);

            
            var viewModel = new MatchStatisticsViewModel
            {
                MatchId = match.Id,
                OpponentName = match.Opponent,
                PlayerStats = allPlayers.Select(player =>
                {
                    var stat = existingStats.FirstOrDefault(s => s.PlayerId == player.Id);
                    return stat ?? new PlayerStatistic
                    {
                        PlayerId = player.Id,
                        MatchId = match.Id,
                        Player = player,
                        ServicePoints = 0,
                        AttackPoints = 0,
                        BlockPoints = 0,
                        PositiveReceptions = 0,
                        Errors = 0
                    };
                }).ToList()
            };

            return View(viewModel);
        }
    }
}