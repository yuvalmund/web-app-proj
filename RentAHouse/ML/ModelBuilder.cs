using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Threading.Tasks;

namespace RentAHouse.ML
{
    public static class ModelBuilder
    {
        // Make sure the 'Copy to Output Directory' property of the text file
        // is set to 'Copy always' (text file should just be imported
        // to the project directiry here
        public const string DATA_PATH = "./ML/Data/trainData.csv";
        public const string MODEL_PATH = "./ML/Data/Model.zip";
        public const string TEST_DATA_PATH = "./ML/Data/testData.csv";

        /*
         * EXPLENATION:
         * The method Train is an async method, so the application continues
         * to respond to the UI. For instance, we can close the application if
         * you don't want to wait for it to finish.
         * Therefore, it returns a task.
        */

        public static async Task<PredictionModel<ApartmentData, AppartmentPricePrediction>> Train()
        {
            // Create a pipeline to load the apartments data
            var pipeline = new LearningPipeline
            {
                // Import the data to the pipeline
                new TextLoader(DATA_PATH).CreateFrom<ApartmentData>(separator: ','),

                // Define the price column as the one to be predicted
                new ColumnCopier(("price", "Label")),

                // Assign the boolean features a numeric value
                new CategoricalOneHotVectorizer("cityID",
                                                "cityAvarageSalary",
                                                "region",
                                                "cityGraduatesPercent",
                                                "RoomsNumber",
                                                "sizeInMeters",
                                                "isThereElivator",
                                                "furnitureInculded",
                                                "isRenovated"),

                // Put all features into a vector
                new ColumnConcatenator(
                "Features",
                "cityID",
                "cityAvarageSalary",
                "region",
                "cityGraduatesPercent",
                "RoomsNumber",
                "sizeInMeters",
                "isThereElivator",
                "furnitureInculded",
                "isRenovated"),

                // Add  learning algorithm to the pipeline 
                new FastTreeRegressor()
            };

            // Train the model according to the information in the pipeline
            PredictionModel<ApartmentData, AppartmentPricePrediction> model = null;

            try
            {
                model = pipeline.Train<ApartmentData, AppartmentPricePrediction>();
            }
            catch (Exception err)
            {
                var x = 1;
            }

            // Save the model to a file.
            // The method is async so the 'await' clause  tells it not to continue
            // until the save finishes, since the save is synchronous
            // In the meantime, control returns to the caller of this async method.
            await model.WriteAsync(MODEL_PATH);

            // After the save is finished return the model
            return model;
        }

        public static string Evaluate(PredictionModel<ApartmentData, AppartmentPricePrediction> model)
        {
            // Load the test data from a file
            var testData = new TextLoader(TEST_DATA_PATH).CreateFrom<ApartmentData>(separator: ',');

            // Make an evaluator matrix
            var evaluator = new RegressionEvaluator();
            RegressionMetrics metrics = evaluator.Evaluate(model, testData);

            // Print the RMS and RSquared are evaluation matrixes of the regression model
            // RMS: The lower it is, the better the model is
            // RSquared: takes values between 0 and 1. The closer its value is to 1, the better the model is. 
            return ($"Rms = {metrics.Rms}" + $", RSquared = {metrics.RSquared}");
        }
    }
}
