using Microsoft.AspNetCore.Mvc;
using VolleyballManager.Data.Models;
using VolleyballManager.Models;
using VolleyballManager.Services; // <--- ПРОМЕНЕНО Е ОТ Services.Interfaces

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

            // Взимаме всички играчи
            var allPlayers = playerService.GetAllPlayers();
            // Взимаме статистиката за този мач
            var existingStats = statisticsService.GetStatisticsByMatch(matchId);

            // Свързваме играчите със статистиката им (ако няма, правим нов празен запис)
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

            // Взимаме всички играчи
            var allPlayers = playerService.GetAllPlayers();
            // Взимаме статистиката за този мач
            var existingStats = statisticsService.GetStatisticsByMatch(matchId);

            // Подготовка на модела (същата логика като Index, но за четене)
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
                        Player = player, // Важно за имената
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