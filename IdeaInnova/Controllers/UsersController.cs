
//using IdeaInnova.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IdeaInnova.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly IdeaInnovaContext _context;

//        public UsersController(IdeaInnovaContext context)
//        {
//            _context = context;
//        }

//        // POST: api/users/login
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] User user)
//        {
//            var existingUser = await _context.Users
//                .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

//            if (existingUser == null)
//            {
//                return Unauthorized("Invalid credentials.");
//            }

//            return Ok(new { Message = "Login successful", Username = existingUser.Username });
//        }

//        // POST: api/users/signup
//        [HttpPost("signup")]
//        public async Task<IActionResult> Signup([FromBody] User user)
//        {
//            var existingUser = await _context.Users
//                .AnyAsync(u => u.Username == user.Username);

//            if (existingUser)
//            {
//                return Conflict("User already exists.");
//            }

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            return Ok(new { Message = "Signup successful" });
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
    public class UsersController : ControllerBase
    {
        private readonly IdeaInnovaContext _context;

        public UsersController(IdeaInnovaContext context)
        {
            _context = context;
        }

        //  POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
          
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new
            {
                Message = "Login successful",
                UserId = existingUser.Id,
                Username = existingUser.Username,
                Role = existingUser.Role
            });
        }

        //  POST: api/users/signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            var existingUser = await _context.Users
                .AnyAsync(u => u.Username == user.Username);

            if (existingUser)
            {
                return Conflict("User already exists.");
            }

            user.Role ??= "User"; // Default role
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Signup successful", UserId = user.Id });
        }

        //  GET: api/users - List all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //  GET: api/users/{id} - Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return user;
        }

        //  GET: api/users/{id}/ideas - Get user's submitted ideas
        [HttpGet("{id}/ideas")]
        public async Task<ActionResult<IEnumerable<Idea>>> GetUserIdeas(int id)
        {
            var ideas = await _context.Ideas
                .Where(i => i.UserId == id)
                .OrderByDescending(i => i.SubmissionDate)
                .ToListAsync();

            return ideas;
        }

        //  GET: api/users/leaderboard - Leaderboard (if not in IdeasController)
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            //if (HttpContext.Session.GetString("Username") == null)
            //    return RedirectToAction("Login", "Account");

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

        //  GET: api/users/{username}/id - Get User ID from username
        [HttpGet("{username}/id")]
        public async Task<IActionResult> GetUserId(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound();

            return Ok(new { user.Id });
        }
    }
}
