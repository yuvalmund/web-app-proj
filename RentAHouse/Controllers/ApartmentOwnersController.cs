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
    public class ApartmentOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApartmentOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApartmentOwner.ToListAsync());
        }

        // GET: ApartmentOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentOwner = await _context.ApartmentOwner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentOwner == null)
            {
                return NotFound();
            }

            return View(apartmentOwner);
        }

        // GET: ApartmentOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApartmentOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<string> Create([Bind("ID,userName,password,firstName,lastName,mail,rate")] ApartmentOwner apartmentOwner)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        apartmentOwner.rate = 0;
        //        _context.Add(apartmentOwner);
        //        await _context.SaveChangesAsync();
        //        //return RedirectToAction(nameof(Index));
        //    }

        //    return "Done!";
        //}

        [HttpPost]
        public async Task<string> Create(string userName, string password, string firstName,string lastName,string mail)
        {
            ApartmentOwner apartmentOwner = new ApartmentOwner();
            apartmentOwner.userName = userName;
            apartmentOwner.password = password;
            apartmentOwner.firstName = firstName;
            apartmentOwner.lastName = lastName;
            apartmentOwner.mail = mail;

            if (ModelState.IsValid)
            {
                apartmentOwner.rate = 0;
                _context.Add(apartmentOwner);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            return "Done!";
        }

        // GET: ApartmentOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentOwner = await _context.ApartmentOwner.FindAsync(id);
            if (apartmentOwner == null)
            {
                return NotFound();
            }
            return View(apartmentOwner);
        }

        // POST: ApartmentOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,userName,password,firstName,lastName,mail,rate")] ApartmentOwner apartmentOwner)
        {
            if (id != apartmentOwner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartmentOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentOwnerExists(apartmentOwner.ID))
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
            return View(apartmentOwner);
        }

        // GET: ApartmentOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentOwner = await _context.ApartmentOwner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartmentOwner == null)
            {
                return NotFound();
            }

            return View(apartmentOwner);
        }

        // POST: ApartmentOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartmentOwner = await _context.ApartmentOwner.FindAsync(id);
            _context.ApartmentOwner.Remove(apartmentOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentOwnerExists(int id)
        {
            return _context.ApartmentOwner.Any(e => e.ID == id);
        }
    }
}
