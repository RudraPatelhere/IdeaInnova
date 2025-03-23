using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdeaInnovaContext _context;

        public HomeController(IdeaInnovaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ideas = await _context.Ideas.Include(i => i.User).ToListAsync();
            var trending = ideas.OrderByDescending(i => i.Votes).Take(3).ToList();
            ViewBag.AllIdeas = ideas;
            ViewBag.Trending = trending;
            return View();
        }
    }
}
