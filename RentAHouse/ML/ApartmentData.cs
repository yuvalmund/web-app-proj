using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAHouse.ML
{
    // Defines the data model, that is read from the CSV files
    public class ApartmentData
    {
        [Column("0")]
        public int cityID;

        [Column("1")]
        public float cityAvarageSalary;

        [Column("2")]
        public int region;

        [Column("3")]
        public float cityGraduatesPercent;

        [Column("4")]
        public int RoomsNumber;

        [Column("5")]
        public int sizeInMeters;

        [Column("6")]
        public bool isThereElivator;

        [Column("7")]
        public bool furnitureInculded;

        [Column("8")]
        public bool isRenovated;

        [Column("9")]
        public float price;
    }
}
