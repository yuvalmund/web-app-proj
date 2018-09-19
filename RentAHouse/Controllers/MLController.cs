using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using RentAHouse.Data;

namespace RentAHouse.Controllers
{
    public class MLController : Controller
    {
        public PredictionModel<ML.ApartmentData, ML.AppartmentPricePrediction> model;
        private IConfiguration Configuration;

        private readonly ApplicationDbContext _context;

        public MLController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void createCSVFiles()
        {
            // Create CSV for ML
            string constr = this.Configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ct.ID, ct.avarageSalary, ct.region, ct.GraduatesPercents,ap.roomsNumber, ap.size, ap.isRenovatetd, ap.isThereElivator, ap.furnitureInculded, ap.price FROM dbo.Apartment ap, dbo.City ct where ct.ID = ap.cityID"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string trainCSV = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                trainCSV += column.ColumnName + ',';
                            }

                            //Add new line.
                            trainCSV += "\r\n";

                            Random rnd = new Random();
                            string currCSV;

                            // Create a matching tesh CSV
                            string testCSV = trainCSV.Substring(0, trainCSV.Length);

                            foreach (DataRow row in dt.Rows)
                            {
                                // Randomly decide which CSV to add it to
                                if (rnd.Next(0, 2) > 0)
                                    currCSV = trainCSV;
                                else
                                    currCSV = testCSV;

                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    currCSV += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                currCSV += "\r\n";
                            }

                            //Download the CSV files.
                            System.IO.File.WriteAllText("ML\\Data\\trainData.csv", trainCSV);
                            System.IO.File.WriteAllText("ML\\Data\\testData.csv", testCSV);
                        }
                    }
                }
            }
        }

        // GET: ML
        [HttpGet]
        public async Task<string> TrainModel()
        {
            this.createCSVFiles();
            this.model = await ML.ModelBuilder.Train();

            return ML.ModelBuilder.Evaluate(model);
        }


        [HttpGet]
        public async Task<string> predict(int inCityID,
                              float inCityAvarageSalary,
                              int inRegion,
                              float inCityGraduatesPercent,
                              int inRoomsNumber,
                              int inSizeInMeters,
                              bool inIsThereElivator,
                              bool inFurnitureInculded,
                              bool inIsRenovated)
        {

            Models.City currCity = _context.City.Where(c => c.ID == inCityID).First();


            ML.ApartmentData newExample = new ML.ApartmentData
            {
                cityID = inCityID,
                cityAvarageSalary = currCity.avarageSalary,
                region = (int)currCity.region,
                cityGraduatesPercent = currCity.GraduatesPercents,
                RoomsNumber = inRoomsNumber,
                sizeInMeters = inSizeInMeters,
                isThereElivator = inIsThereElivator,
                furnitureInculded = inFurnitureInculded,
                isRenovated = inIsRenovated
            };

            ML.AppartmentPricePrediction prediction = this.model.Predict(newExample);

            return prediction.ToString();
        }
    }
}