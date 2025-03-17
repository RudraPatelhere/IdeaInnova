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
            return await _context.Ideas.ToListAsync();
        }

        // ✅ POST: api/ideas - Submit a new idea
        [HttpPost]
        public async Task<ActionResult<Idea>> SubmitIdea([FromBody] Idea idea)
        {
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

            // Implement voting mechanism (if needed)
            idea.SubmissionDate = System.DateTime.Now;  // Placeholder update

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ DELETE: api/ideas/{id} - Delete an idea
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
    }
}
