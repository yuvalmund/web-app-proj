using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAHouse.Models
{
    public class City
    {
        public int ID { get; set; }
        public string cityName { get; set; }
        public int GraduatesPercents { get; set; }
        public string mayor { get; set; }
        public int avarageSalary { get; set; }
        public int numOfResidents { get; set; }
        public District region { get; set; }
    }
}
