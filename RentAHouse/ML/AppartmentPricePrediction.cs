using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAHouse.ML
{
    public class AppartmentPricePrediction
    {
        //[ColumnName("price")]
        [ColumnName("Score")]
        public float price;
    }
}
