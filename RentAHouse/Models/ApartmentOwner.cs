using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class ApartmentOwner
    {
        public int ID { get; set; }
        [DisplayName("User name")]
        public string userName { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
        [DisplayName("First name")]
        public string firstName { get; set; }
        [DisplayName("Last name")]
        public string lastName { get; set; }
        [DisplayName("Mail")]
        public string mail { get; set; }
        public int rate { get; set; }
        public virtual Apartment[] apartments { get; set; }
    }
}
