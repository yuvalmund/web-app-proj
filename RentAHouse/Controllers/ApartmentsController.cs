using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Data;
using RentAHouse.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Newtonsoft.Json;

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


        [HttpGet]
        public string GetCities(int region)
        {
            List<City> n;
            if (region == (int)District.All)
            {
                n = _context.City.ToList();
            }
            else
            {
                District dis = (District)region;
                n = _context.City.Where(i => i.region == dis).ToList();
            }

            var json = JsonConvert.SerializeObject(n);

            return json;
        }

        public async Task<string> GetAllCites(int region)
        {
            List<City> n;

            n = _context.City.ToList();

            var json = JsonConvert.SerializeObject(n);

            return json;
        }

        [HttpGet]
        public string GetApartments(int cityId, int roomNumber, int minPrice, int maxPrice) {

            var queryApartments =
                from currCity in _context.City
                where cityId == currCity.ID
                join currApartment in _context.Apartment on currCity.ID equals currApartment.city.ID
                join currOwner in _context.ApartmentOwner on currApartment.owner equals currOwner
                select new
                {
                    currOwner.firstName,
                    currOwner.lastName,
                    currOwner.Email,
                    currOwner.rate,
                    currApartment.ID,
                    currApartment.street,
                    currApartment.houseNumber,
                    currApartment.roomsNumber,
                    currApartment.size,
                    currApartment.price,
                    currApartment.cityTax,
                    currApartment.BuildingTax,
                    currApartment.furnitureInculded,
                    currApartment.isRenovatetd,
                    currApartment.arePetsAllowed,
                    currApartment.isThereElivator,
                    currApartment.floor,
                    currCity.cityName,
                    currCity.region,
                    currApartment.EnterDate
                };

            if (roomNumber != -1)
            {
                queryApartments = queryApartments.Where(p => p.roomsNumber == roomNumber);
            }
            if (minPrice != -1)
            {
                queryApartments = queryApartments.Where(p => p.price >= minPrice);
            }
            if (maxPrice != -1)
            {
                queryApartments = queryApartments.Where(p => p.price <= maxPrice);
            }

            return JsonConvert.SerializeObject(queryApartments.ToList());
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
       
        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("ID,city,street,houseNumber,roomsNumber,size,price,cityTax,BuildingTax,furnitureInculded,isRenovatetd,arePetsAllowed,isThereElivator,EnterDate,floor")] Apartment apartment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        List<ApartmentOwner> Owner = _context.ApartmentOwner.Where(i => i.Id == userId).ToList();
        //        apartment.owner = Owner[0];
        //        _context.Add(apartment);
        //        await _context.SaveChangesAsync();
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    return View("~/Views/Home/Index.cshtml");
        //}


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string city, string street, int houseNumber, int roomsNumber, int size,int price,int cityTax,int BuildingTax,bool furnitureInculded,bool isRenovatetd,bool arePetsAllowed,bool isThereElivator,DateTime EnterDate,int floor)
        {
            Apartment apartment = new Apartment();
            apartment.city = _context.City.Where(i => i.ID == Convert.ToInt32(city)).ToList()[0];
            apartment.street = street;
            apartment.houseNumber = houseNumber;
            apartment.roomsNumber = roomsNumber;
            apartment.size = size;
            apartment.price = price;
            apartment.cityTax = cityTax;
            apartment.BuildingTax = BuildingTax;
            apartment.furnitureInculded = furnitureInculded;
            apartment.isRenovatetd = isRenovatetd;
            apartment.arePetsAllowed = arePetsAllowed;
            apartment.isThereElivator = isThereElivator;
            apartment.EnterDate = EnterDate;
            apartment.floor = floor;

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                List<ApartmentOwner> Owner = _context.ApartmentOwner.Where(i => i.Id == userId).ToList();
                apartment.owner = Owner[0];
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public async Task<string> GetApartmentsByOwner()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var queryApartments =
                  from currOwner in _context.ApartmentOwner
                  where currOwner.Id == userId
                  join currApartment in _context.Apartment on currOwner.Id equals currApartment.owner.Id
                  join currCity in _context.City on currApartment.city equals currCity
                  select new
                  {
                      currOwner.firstName,
                      currOwner.lastName,
                      currOwner.Email,
                      currOwner.rate,
                      currApartment.ID,
                      currApartment.street,
                      currApartment.houseNumber,
                      currApartment.roomsNumber,
                      currApartment.size,
                      currApartment.price,
                      currApartment.cityTax,
                      currApartment.BuildingTax,
                      currApartment.furnitureInculded,
                      currApartment.isRenovatetd,
                      currApartment.arePetsAllowed,
                      currApartment.isThereElivator,
                      currApartment.floor,
                      currCity.cityName,
                      currCity.region
                  };

            string MyAp = JsonConvert.SerializeObject(queryApartments.ToList());

            return MyAp;
        }

        // GET: Apartments/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
