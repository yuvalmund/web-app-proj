using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Data;
using RentAHouse.Models;

namespace RentAHouse.Controllers
{
    public class ApartmentViewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentViewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApartmentViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApartmentViews.ToListAsync());
        }

        // GET: ApartmentViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentViews = await _context.ApartmentViews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentViews == null)
            {
                return NotFound();
            }

            return View(apartmentViews);
        }

        // GET: ApartmentViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApartmentViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentViews apartmentViews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartmentViews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartmentViews);
        }

        // GET: ApartmentViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentViews = await _context.ApartmentViews.FindAsync(id);
            if (apartmentViews == null)
            {
                return NotFound();
            }
            return View(apartmentViews);
        }

        // POST: ApartmentViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,date")] ApartmentViews apartmentViews)
        {
            if (id != apartmentViews.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartmentViews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentViewsExists(apartmentViews.ID))
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
            return View(apartmentViews);
        }

        // GET: ApartmentViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentViews = await _context.ApartmentViews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentViews == null)
            {
                return NotFound();
            }

            return View(apartmentViews);
        }

        // POST: ApartmentViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartmentViews = await _context.ApartmentViews.FindAsync(id);
            _context.ApartmentViews.Remove(apartmentViews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentViewsExists(int id)
        {
            return _context.ApartmentViews.Any(e => e.ID == id);
        }

        [HttpPost]
        public async void addClick(int apartment)
        {
            ApartmentViews view = new ApartmentViews();
            view.apartment = _context.Apartment.Where(i => i.ID == apartment).ToList()[0];
            view.date = DateTime.Today;

            Create(view);
            //_context.Add(view);
            //await _context.SaveChangesAsync();
        }
    }
}
