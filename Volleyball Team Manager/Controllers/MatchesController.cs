using Microsoft.AspNetCore.Mvc;
using VolleyballManager.Data.Models;
using VolleyballManager.Services;

namespace VolleyballManager.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchService matchService;

        public MatchesController(IMatchService _matchService)
        {
            matchService = _matchService;
        }

        public IActionResult Index()
        {
            var matches = matchService.GetAllMatches();
            return View(matches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                matchService.AddMatch(match);
                return RedirectToAction("Index");
            }
            return View(match);
        }


        public IActionResult Edit(int id)
        {
            var match = matchService.GetById(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        [HttpPost]
        public IActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                matchService.UpdateMatch(match);
                return RedirectToAction("Index");
            }
            return View(match);
        }

        public IActionResult Delete(int id)
        {
            var match = matchService.GetById(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            matchService.DeleteMatch(id);
            return RedirectToAction("Index");
        }
    }
}