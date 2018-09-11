using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        public virtual ApartmentOwner owner { get; set; }
        [DisplayName("City")]
        public virtual City city { get; set; }
        [DisplayName("Street")]
        public string street { get; set; }
        [DisplayName("House number")]
        public int houseNumber { get; set; }
        [DisplayName("Room number")]
        public int roomsNumber { get; set; }
        [DisplayName("Size")]
        public int size { get; set; }
        [DisplayName("Price")]
        public int price { get; set; }

        [DisplayName("City tax")]
        public int cityTax { get; set; }
        [DisplayName("Building Tax")]
        public int BuildingTax { get; set; }
        [DisplayName("Furniture Inculded?")]
        public bool furnitureInculded { get; set; }
        [DisplayName("Renovatetd?")]
        public bool isRenovatetd { get; set; }
        [DisplayName("Are Pets Allowed?")]
        public bool arePetsAllowed { get; set; }
        [DisplayName("Is There Elivator?")]
        public bool isThereElivator { get; set; }
        [DisplayName("Enter Date")]
        public DateTime EnterDate { get; set; }
        [DisplayName("Floor")]
        public int floor { get; set; }
        public virtual ApartmentImage[] images { get; set; }


    }
}
