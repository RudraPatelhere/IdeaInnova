//using IdeaInnova.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IdeaInnova.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class IdeasController : ControllerBase
//    {
//        private readonly IdeaInnovaContext _context;

//        public IdeasController(IdeaInnovaContext context)
//        {
//            _context = context;
//        }

//        // ✅ GET: api/ideas - Retrieve all ideas
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Idea>>> GetIdeas()
//        {
//            return await _context.Ideas.ToListAsync();
//        }

//        // ✅ POST: api/ideas - Submit a new idea
//        [HttpPost]
//        public async Task<ActionResult<Idea>> SubmitIdea([FromBody] Idea idea)
//        {
//            if (idea == null || string.IsNullOrEmpty(idea.Title) || string.IsNullOrEmpty(idea.Description))
//            {
//                return BadRequest("Invalid idea data.");
//            }

//            _context.Ideas.Add(idea);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetIdeas), new { id = idea.Id }, idea);
//        }

//        // ✅ PATCH: api/ideas/{id} - Upvote an idea
//        [HttpPatch("{id}")]
//        public async Task<IActionResult> UpvoteIdea(int id)
//        {
//            var idea = await _context.Ideas.FindAsync(id);
//            if (idea == null)
//            {
//                return NotFound();
//            }

//            idea.Votes += 1; // Increment vote count

//            await _context.SaveChangesAsync();
//            return NoContent();
//        }

//        // ✅ DELETE: api/ideas/{id} - Delete an idea
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteIdea(int id)
//        {
//            var idea = await _context.Ideas.FindAsync(id);
//            if (idea == null)
//            {
//                return NotFound();
//            }

//            _context.Ideas.Remove(idea);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }






//    }
//}









using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdeaInnova.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private readonly IdeaInnovaContext _context;

        public IdeasController(IdeaInnovaContext context)
        {
            _context = context;
        }

        // ✅ GET: api/ideas - Retrieve all ideas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> GetIdeas()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            return await _context.Ideas.Include(i => i.User).ToListAsync();
        }

        // ✅ POST: api/ideas - Submit a new idea
        [HttpPost]
        public async Task<ActionResult<Idea>> SubmitIdea([FromBody] Idea idea)
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            if (idea == null || string.IsNullOrEmpty(idea.Title) || string.IsNullOrEmpty(idea.Description))
            {
                return BadRequest("Invalid idea data.");
            }

            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIdeas), new { id = idea.Id }, idea);
        }

        // ✅ PATCH: api/ideas/{id} - Upvote an idea
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpvoteIdea(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }

            idea.Votes += 1;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ DELETE: api/ideas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🆕 GET: api/ideas/trending - Top 3 ideas by vote
        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<Idea>>> GetTrendingIdeas()
        {
            var trending = await _context.Ideas
                .OrderByDescending(i => i.Votes)
                .Take(3)
                .ToListAsync();

            return trending;
        }

        // 🆕 GET: api/ideas/leaderboard - User leaderboard stats
        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<object>>> GetLeaderboard()
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

            return Ok(leaderboard);
        }

        // 🆕 GET: api/ideas/user/{userId} - Get all ideas by a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Idea>>> GetIdeasByUser(int userId)
        {
            var userIdeas = await _context.Ideas
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.SubmissionDate)
                .ToListAsync();

            return userIdeas;
        }
    }
}

