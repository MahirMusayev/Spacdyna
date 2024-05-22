using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spacdyna.DAL;
using Spacdyna.Models;
using Spacdyna.ViewModel;

namespace Spacdyna.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController(SpacContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Offers> off = await _context.Offers.ToListAsync();
            return View(off);
        }
        public async Task<IActionResult> Creat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Creat(CreateOfferVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Offers.AddAsync(new Offers
            {
                Name = vm.Name,
                Description = vm.Description,
                ImageUrl = vm.ImageUrl,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Offers offers = await _context.Offers.FirstOrDefaultAsync(x=>x.Id==id);
            if (offers == null)
            {
                return NotFound();
            }
            return View(offers);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CreateOfferVM vm)
        {

            if (id == null || id < 1) return BadRequest();
            Offers offers = await _context.Offers.FirstOrDefaultAsync(x => x.Id == id);
            if (offers == null) return NotFound();
            offers.Name = vm.Name;
            offers.Description = vm.Description;    
            offers.ImageUrl = vm.ImageUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id < 1)return BadRequest();
            Offers offers = await _context.Offers.FirstOrDefaultAsync(x => x.Id == id);
            if (offers == null) return NotFound();
            _context.Remove(offers);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
