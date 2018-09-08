﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Data;
using RentAHouse.Models;
using Newtonsoft.Json;
using System;

namespace RentAHouse.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var citiesList = new MultiSelectList(_context.City.Select(i => i), "ID", "cityName");
            ViewBag.Cities = citiesList;

            return View(await _context.Apartment.ToListAsync());
        }


        [HttpPost]
        public async Task<string> Index(int region)
        {
            List<City> n;
            if (region == (int)District.All)
            {
                n = _context.City.ToList();
            }
            else
            {
                try
                {
                    n = _context.City.Where(i => i.region == (District)region).ToList();
                }
                catch (Exception ex)
                {
                    int i = 10;
                }
                finally
                {
                    n = _context.City.ToList();
                }
            }

            var json = JsonConvert.SerializeObject(n);

            return json;
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,street,houseNumber,roomsNumber,size,price,cityTax,BuildingTax,furnitureInculded,isRenovatetd,arePetsAllowed,isThereElivator,EnterDate,floor")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,street,houseNumber,roomsNumber,size,price,cityTax,BuildingTax,furnitureInculded,isRenovatetd,arePetsAllowed,isThereElivator,EnterDate,floor")] Apartment apartment)
        {
            if (id != apartment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ID))
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
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartment.FindAsync(id);
            _context.Apartment.Remove(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartment.Any(e => e.ID == id);
        }

        public void onRegionSelected(int region)
        {
            var citiesList = new MultiSelectList(null);
            if (region == (int)District.All)
            {
                citiesList = new MultiSelectList(_context.City.Select(i => i), "ID", "cityName");
            }
            else
            {
                citiesList = new MultiSelectList(_context.City.Select(i => i.region == (District)region), "ID", "cityName");
            }
            ViewBag.Cities = citiesList; 
        }
    }
}
