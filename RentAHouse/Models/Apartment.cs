using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        public virtual ApartmentOwner owner { get; set; }
        public virtual City city { get; set; }
        public string street { get; set; }
        public int houseNumber { get; set; }
        public int roomsNumber { get; set; }
        public int size { get; set; }
        public int price { get; set; }
        public int cityTax { get; set; }
        public int BuildingTax { get; set; }
        public bool furnitureInculded { get; set; }
        public bool isRenovatetd { get; set; }
        public bool arePetsAllowed { get; set; }
        public bool isThereElivator { get; set; }
        public DateTime MyProperty { get; set; }
        public int floor { get; set; }
        public virtual ApartmentImage[] images { get; set; }


    }
}
