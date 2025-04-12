using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{ // This controller handles the Razor view-based actions for submitting and voting on ideas
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

            
            var user = _context.Users
                .FirstOrDefault(u => u.Username == HttpContext.Session.GetString("Username"));

            ViewBag.User = user;  
            return View();
        }


        // POST: /Ideas/Submit
        [HttpPost]
        public async Task<IActionResult> Submit(Idea idea)
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
               
                var user = _context.Users.FirstOrDefault(u => u.Username == HttpContext.Session.GetString("Username"));
                ViewBag.User = user;
                return View(idea);
            }

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == HttpContext.Session.GetString("Username"));
            idea.UserId = dbUser?.Id;
            _context.Ideas.Add(idea);//save the new idea to the database 
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
            //for passing the list of ideas to the vote.cshtml view
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
                idea.Votes += 1;//Increase vot count 
                await _context.SaveChangesAsync();
            }
            //It will redirect back to the voting page 
            return RedirectToAction("Vote");
        }
    }
}