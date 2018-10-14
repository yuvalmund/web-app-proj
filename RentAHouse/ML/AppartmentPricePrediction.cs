using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAHouse.ML
{
    // Defines the prediction data model
    public class AppartmentPricePrediction
    {
        //[ColumnName("price")]
        [ColumnName("Score")]
        public float price;
    }
}
