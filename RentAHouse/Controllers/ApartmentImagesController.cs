using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Data;

namespace RentAHouse.Models
{
    public class ApartmentImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApartmentImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApartmentImage.ToListAsync());
        }

        // GET: ApartmentImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentImage = await _context.ApartmentImage
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentImage == null)
            {
                return NotFound();
            }

            return View(apartmentImage);
        }

        // GET: ApartmentImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApartmentImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,imageFileName")] ApartmentImage apartmentImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartmentImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartmentImage);
        }

        // GET: ApartmentImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentImage = await _context.ApartmentImage.FindAsync(id);
            if (apartmentImage == null)
            {
                return NotFound();
            }
            return View(apartmentImage);
        }

        // POST: ApartmentImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,imageFileName")] ApartmentImage apartmentImage)
        {
            if (id != apartmentImage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartmentImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentImageExists(apartmentImage.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(apartmentImage);
        }

        // GET: ApartmentImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentImage = await _context.ApartmentImage
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentImage == null)
            {
                return NotFound();
            }

            return View(apartmentImage);
        }

        // POST: ApartmentImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartmentImage = await _context.ApartmentImage.FindAsync(id);
            _context.ApartmentImage.Remove(apartmentImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentImageExists(int id)
        {
            return _context.ApartmentImage.Any(e => e.ID == id);
        }
    }
}
