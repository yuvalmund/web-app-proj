using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class ApartmentImage
    {
        public int ID { get; set; }
        public virtual Apartment apartment { get; set; }
        public string imageFileName { get; set; }
    }
}
