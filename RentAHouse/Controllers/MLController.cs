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
using RentAHouse.ML;
using Microsoft.AspNetCore.Authorization;

namespace RentAHouse.Controllers
{
    public class MLController : Controller
    {
        // Needed for the creation of CSV - contains the connection string to the DB
        private IConfiguration Configuration;

        private readonly ApplicationDbContext _context;

        public MLController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.Configuration = configuration;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public void createCSVFiles()
        {
            // Get the connection string
            string constr = this.Configuration.GetConnectionString("DefaultConnection");

            // Open an SQL Connection
            using (SqlConnection con = new SqlConnection(constr))
            {
                // Define a query to recieve the data needed for the CSV files
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ct.ID, ct.avarageSalary, ct.region, ct.GraduatesPercents,ap.roomsNumber, ap.size, ap.isThereElivator, ap.furnitureInculded, ap.isRenovatetd, ap.price FROM dbo.Apartment ap, dbo.City ct where ct.ID = ap.cityID"))
                {
                    // Connect to the Database
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        // Create a data table
                        using (DataTable dt = new DataTable())
                        {
                            // Fill the table with the results of the query
                            sda.Fill(dt);

                            //ccreate two strings to be written to who CSV files: taining data and test data
                            string[] CSVs = new string[2];
                            CSVs[0] = string.Empty;
                            CSVs[1] = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                CSVs[0] += column.ColumnName + ',';
                                CSVs[1] += column.ColumnName + ',';
                            }

                            //Add new line.
                            CSVs[0] += "\r\n";
                            CSVs[1] += "\r\n";

                            Random rnd = new Random();

                            foreach (DataRow row in dt.Rows)
                            {
                                // Randomly decide which CSV to add it to
                                int nIndex = rnd.Next(0, 2);
                                
                                // Go throgh the rows
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data column.
                                    CSVs[nIndex] += (int.Parse(row[column.ColumnName].ToString().Replace("False",  "0").Replace("True", "1"))).ToString() + ',';
                                }

                                //Add new line.
                                CSVs[nIndex] += "\r\n";
                            }

                            //Download the CSV files.
                            System.IO.File.WriteAllText("ML\\Data\\trainData.csv", CSVs[0]);
                            System.IO.File.WriteAllText("ML\\Data\\testData.csv", CSVs[0]);
                        }
                    }
                }
            }
        }

        // GET: ML
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<string> TrainModel()
        {
            // Create the files with the training data and test data
            this.createCSVFiles();

            // Build the model
            PredictionModel<ML.ApartmentData, ML.AppartmentPricePrediction> model = await ML.ModelBuilder.Train();

            // Evaluate the built model and return its metrics
            return ML.ModelBuilder.Evaluate(model);
        }

        [HttpGet]
        // gets appartment data and predicts a price
        public async Task<string> predict(int inCityID,
                              int inRoomsNumber,
                              int inSizeInMeters,
                              bool inIsThereElivator,
                              bool inFurnitureInculded,
                              bool inIsRenovated)
        {
            // Get the city in which the apartment is located
            Models.City currCity = _context.City.Where(c => c.ID == inCityID).First();
            
            // create an ApartmentData object from the given parameters
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

            //Read the model from the zip file
            var model = await PredictionModel.ReadAsync<ApartmentData, AppartmentPricePrediction>(ModelBuilder.MODEL_PATH);

            // Make the prediction
            ML.AppartmentPricePrediction prediction = model.Predict(newExample);

            return ((int)prediction.price).ToString();
        }
    }
}