using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spacdyna.Models;

namespace Spacdyna.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Register(AppUser user)
        {
            if (!ModelState.IsValid) return View(user);

            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {

                return View(user);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUser loginUser)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
                return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, loginUser.PasswordHash, true, true);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

