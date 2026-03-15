using Microsoft.AspNetCore.Mvc;
using VolleyballManager.Data; // Твоят namespace

namespace VolleyballManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            
            var playersCount = context.Players.Count();
            var matchesCount = context.Matches.Count();

            
            var lastMatch = context.Matches.OrderByDescending(m => m.Date).FirstOrDefault();

            
            ViewBag.PlayersCount = playersCount;
            ViewBag.MatchesCount = matchesCount;
            ViewBag.LastMatch = lastMatch;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}