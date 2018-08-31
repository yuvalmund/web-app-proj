using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class ApartmentViews
    {
        public int ID { get; set; }
        public virtual Apartment apartment { get; set; }
        public DateTime date { get; set; }
    }
}
