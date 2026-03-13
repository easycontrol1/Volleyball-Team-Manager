using Microsoft.AspNetCore.Mvc;
using VolleyballManager.Data.Models;
using VolleyballManager.Services;
namespace VolleyballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService _playerService)
        {
            playerService = _playerService;
        }

        // Списък
        public IActionResult Index()
        {
            var players = playerService.GetAllPlayers();
            return View(players);
        }

        // Форма за добавяне
        public IActionResult Create()
        {
            return View();
        }

        // Записване
        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                playerService.AddPlayer(player);
                return RedirectToAction("Index");
            }
            return View(player);
        }
    }
}