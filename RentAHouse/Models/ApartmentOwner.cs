using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class ApartmentOwner
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public int rate { get; set; }
        public virtual Apartment[] apartments { get; set; }
    }
}
