using IdeaInnova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Controllers
{
    public class AccountController : Controller
    {
        private readonly IdeaInnovaContext _context;

        public AccountController(IdeaInnovaContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();
        public IActionResult Signup() => View();

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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
