using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class ApartmentOwner : IdentityUser
    { 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int rate { get; set; }
        public virtual Apartment[] apartments { get; set; }
    }
}
