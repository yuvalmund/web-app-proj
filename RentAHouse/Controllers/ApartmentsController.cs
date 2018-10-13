using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Data;
using RentAHouse.Models;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace RentAHouse.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // needed for the creation of the CSV files, to get the connection string to DB
        private IConfiguration Configuration;

        public ApartmentsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var citiesList = new MultiSelectList(_context.City.Select(i => i), "ID", "cityName");
            ViewBag.Cities = citiesList;

            return View(await _context.Apartment.ToListAsync());
        }

        // GET: Apartments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            return View(await _context.Apartment.ToListAsync());
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
        [Authorize]
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
            createCSVs();

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

        [HttpGet]
        public void createCSVs()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string constr = this.Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT count(apartmentID) click, date from dbo.ApartmentViews vs, dbo.Apartment ap WHERE vs.apartmentID = ap.ID AND ap.ownerId = '" + userId + "' group by date"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            System.IO.File.WriteAllText("wwwroot\\data\\graph.csv", csv);
                        }
                    }
                }
            }
        }

        public string GetApartmentByCity()
        {
            var result = from ap in _context.Apartment
                         join city in _context.City on ap.city.ID equals city.ID
                         group new
                         {
                             ap.ID,
                             city.cityName,
                             ap.street,
                             ap.houseNumber,
                             ap.roomsNumber,
                             ap.size,
                             ap.price,
                             ap.cityTax,
                             ap.BuildingTax,
                             ap.furnitureInculded,
                             ap.isRenovatetd,
                             ap.arePetsAllowed,
                             ap.isThereElivator,
                             ap.EnterDate,
                             ap.floor
                         }
                         by ap.city.cityName into grouedAps
                         select grouedAps;

            return JsonConvert.SerializeObject(result);
        }
    }
}
