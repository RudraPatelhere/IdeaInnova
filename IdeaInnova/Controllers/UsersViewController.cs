using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdeaInnova.Controllers
{
    public class UsersViewController : Controller
    {
        private readonly IdeaInnovaContext _context;

        public UsersViewController(IdeaInnovaContext context)
        {
            _context = context;
        }

        // GET: /UsersView/Profile
        public async Task<IActionResult> Profile()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
                return RedirectToAction("Login", "Account");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var ideas = await _context.Ideas.Where(i => i.UserId == user.Id).ToListAsync();

            ViewBag.User = user;
            ViewBag.Ideas = ideas;
            return View(); //Will look in Views/UsersView/Profile.cshtml
        }


        // GET: /UsersView/Leaderboard
        public async Task<IActionResult> Leaderboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            var leaderboard = await _context.Users
                .Select(u => new
                {
                    u.Username,
                    IdeaCount = _context.Ideas.Count(i => i.UserId == u.Id),
                    TotalVotes = _context.Ideas.Where(i => i.UserId == u.Id).Sum(i => i.Votes)
                })
                .OrderByDescending(u => u.TotalVotes)
                .ToListAsync();

            ViewBag.Leaderboard = leaderboard;
            return View();
        }
    }
}