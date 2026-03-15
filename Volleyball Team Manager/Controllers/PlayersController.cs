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

        // Редакция
        public IActionResult Edit(int id)
        {
            var player = playerService.GetById(id);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // Детайли

        public IActionResult Details(int id)
        {
            var player = playerService.GetById(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }
        // ИЗТРИВАНЕ

        // 1. Показва формата за потвърждение (GET)
        public IActionResult Delete(int id)
        {
            var player = playerService.GetById(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // 2. Извършва реалното изтриване (POST)
        [HttpPost]
        [ActionName("Delete")] // Това казва на ASP.NET да го третира като "Delete" действие
        public IActionResult DeleteConfirmed(int id)
        {
            playerService.DeletePlayer(id);
            return RedirectToAction("Index");
        }
    }
}