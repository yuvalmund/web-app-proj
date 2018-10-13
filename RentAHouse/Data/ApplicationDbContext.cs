using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentAHouse.Models;

namespace RentAHouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RentAHouse.Models.Apartment> Apartment { get; set; }
        public DbSet<RentAHouse.Models.ApartmentOwner> ApartmentOwner { get; set; }
        public DbSet<RentAHouse.Models.ApartmentViews> ApartmentViews { get; set; }
        public DbSet<RentAHouse.Models.City> City { get; set; }
    }
}
