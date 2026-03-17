using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolleyballManager.Data;
using VolleyballManager.Models;

namespace VolleyballManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var lastMatch = context.Matches.OrderByDescending(m => m.Date).FirstOrDefault();

            var model = new AdminDashboardViewModel
            {
                TotalPlayers = context.Players.Count(),
                TotalMatches = context.Matches.Count(),
                TotalUsers = userManager.Users.Count(),
                LastMatchOpponent = lastMatch?.Opponent ?? "Няма мачове",
                Users = userManager.Users.ToList()
            };

            return View(model);
        }

        // ИЗТРИВАНЕ НА ПОТРЕБИТЕЛ
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        // БУТОН ЗА НУЛИРАНЕ НА БАЗАТА (Удобно за тестове)
        [HttpPost]
        public IActionResult ResetDatabase()
        {
            // Изтриваме само данните за играчи и мачове, потребителите остават
            context.PlayerStatistics.RemoveRange(context.PlayerStatistics);
            context.Matches.RemoveRange(context.Matches);
            context.Players.RemoveRange(context.Players);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}