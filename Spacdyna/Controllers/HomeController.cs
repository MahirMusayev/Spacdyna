using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spacdyna.DAL;
using Spacdyna.Models;


namespace Spacdyna.Controllers
{
    public class HomeController(SpacContext _context ) : Controller
    {

        public async Task<IActionResult> Index()
        {

            return View(await _context.Offers.ToListAsync());
        }

      
    }
}
