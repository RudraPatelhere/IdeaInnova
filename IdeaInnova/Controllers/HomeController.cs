using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{ // This is the controller for handling the home page logic
    public class HomeController : Controller
    {// This constructor to initialize the database context via dependency injection
        private readonly IdeaInnovaContext _context;

        public HomeController(IdeaInnovaContext context)
        {
            _context = context;
        }
        // GET: Home Page
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