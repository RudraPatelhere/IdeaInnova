using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{
    public class IdeasViewController : Controller
    {
        private readonly IdeaInnovaContext _context;

        public IdeasViewController(IdeaInnovaContext context)
        {
            _context = context;
        }

        // GET: /Ideas/Submit
        public IActionResult Submit()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            return View(); // Views/Ideas/Submit.cshtml
        }

        // POST: /Ideas/Submit
        [HttpPost]
        public async Task<IActionResult> Submit(Idea idea)
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == HttpContext.Session.GetString("Username"));
            idea.UserId = user?.Id;
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();

            return RedirectToAction("Submitted");
        }

        // GET: /Ideas/Submitted
        public IActionResult Submitted()
        {
            return View(); // Views/Ideas/Submitted.cshtml
        }

        // GET: /Ideas/Vote
        public async Task<IActionResult> Vote()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            var ideas = await _context.Ideas.ToListAsync();
            return View(ideas); // Views/Ideas/Vote.cshtml
        }

        // POST: /Ideas/Upvote/5
        [HttpPost]
        public async Task<IActionResult> Upvote(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea != null)
            {
                idea.Votes += 1;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Vote");
        }
    }
}
