using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentAHouse.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAHouse.Controllers
{
    public class DistrictsController : Controller
    {
        public string getDistrictEnum()
        {
            return JsonConvert.SerializeObject(Enum.GetValues(typeof(District)), new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
