﻿
using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdeaInnova.Controllers { 
// It defines the route as "api/ideas" and marks this as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private readonly IdeaInnovaContext _context;

        public IdeasController(IdeaInnovaContext context)
        {
            _context = context;
        }

        // GET: api/ideas 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> GetIdeas()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            return await _context.Ideas.Include(i => i.User).ToListAsync();
        }

        // POST: api/ideas 
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

        //  PATCH: api/ideas/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<Idea>> UpvoteIdea(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
                return NotFound();

            idea.Votes += 1;
            await _context.SaveChangesAsync();

            // Return the updated idea (200 OK with JSON body)
            return Ok(idea);
        }


        //DELETE: api/ideas/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Idea>> DeleteIdea(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
                return NotFound();

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();

            // Return the deleted idea so client can confirm
            return Ok(idea);
        }


        //GET: api/ideas/trending 
        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<Idea>>> GetTrendingIdeas()
        {
            var trending = await _context.Ideas
                .OrderByDescending(i => i.Votes)
                .Take(3)
                .ToListAsync();

            return trending;
        }

        // GET: api/ideas/leaderboard
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

        //GET: api/ideas/user/{userId}
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