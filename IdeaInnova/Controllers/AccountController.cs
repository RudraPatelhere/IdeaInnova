using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{ // This controller handles the user authentication 
    public class AccountController : Controller
    {
        private readonly IdeaInnovaContext _context;
        // This is the constructor injection of the database context
        public AccountController(IdeaInnovaContext context)
        {
            _context = context;
        }
        // GET: Login view
        public IActionResult Login() => View();
        // GET: Signup view
        public IActionResult Signup() => View();
        // POST: Login logic
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }
        // POST: Signup logic
        [HttpPost]
        public async Task<IActionResult> Signup(string username, string password, string role)
        {
            var existing = await _context.Users.AnyAsync(u => u.Username == username);
            if (existing)
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            _context.Users.Add(new User { Username = username, Password = password, Role = role });
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        // GET: Logout logic
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//It clears all the session data 
            return RedirectToAction("Login");//redirect to the logout page
        }
    }
}