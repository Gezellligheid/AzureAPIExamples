using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsExamples
{
    public static class GET
    {


        /*
         *
         *  ROUTES CAN BE CHANGED HOWEVER YOU WANT
         *
         *
         */


        /*
         *
         *  Simple get function without parameters
         *
         */
        [FunctionName("GET_noParams")]
        public static async Task<IActionResult> Get_noParams(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getroute")] HttpRequest req,
            ILogger log)
        {
            try
            {
                //Can also return the structure of a class
                return new OkObjectResult("I returned something :)");
            }
            catch (Exception e)
            {
                //Returned http status code
                return new StatusCodeResult(500);
            }
            
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /*
        *
        *  Simple get function with parameters
        *
        */
        [FunctionName("GET_Params")]
        public static async Task<IActionResult> Get_Params(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getroute/{getal1}/{getal2}")] HttpRequest req,
            ILogger log,
            int getal1,
            int getal2)
        {
            try
            {
                //Using the parameters in the sum
                int result = getal1 + getal2;

                //Can also return the structure of a class
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                //Returned http status code
                return new StatusCodeResult(500);
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /*
        *
        *  Simple get function without parameters
        *  Using a database
        *  Install System.Data.SQLClient via NuGet
        *
        */
        [FunctionName("GET_noParams_withDB")]
        public static async Task<IActionResult> Get_noParams_withDB(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getroute/bezoekers")] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<string> names = new List<string>();
                /*
                 *
                 * Dont forget to add your ConnectionString to local.settings.json and changint the name under here
                 * Can be found in the Azure portal
                 *
                 */

                string connection = Environment.GetEnvironmentVariable("CHANGE_ME");

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {

                    await sqlConnection.OpenAsync();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sqlConnection;

                        //Your SQL query
                        command.CommandText = $"SELECT * FROM Bezoekers";

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {

                            names.Add(reader["name"].ToString());
                            // string column1 = reader["name"].ToString();
                            // int column2 = int.Parse(reader["id"].ToString());
                            /*
                             *
                             * Will loop every row in within the result of the query
                             * Do whatevery you want here
                             * Create a list of objects, add data to it and return the data to the API
                             *
                             */
                        }
                    }

                }
                //Can also return the structure of a class
                return new OkObjectResult(names);
            }
            catch (Exception e)
            {
                //Returned http status code
                return new StatusCodeResult(500);
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /*
        *
        *  Simple get function without parameters
        *  Using a database and parameters
        *  Install System.Data.SQLClient via NuGet
        *
        */
        [FunctionName("GET_noParams_withDB")]
        public static async Task<IActionResult> Get_Params_withDB(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getroute/bezoekers/{day}")] HttpRequest req,
            string day,
            ILogger log)
        {
            try
            {
                List<string> names = new List<string>();
                /*
                 *
                 * Dont forget to add your ConnectionString to local.settings.json and changint the name under here
                 * Can be found in the Azure portal
                 *
                 */

                string connection = Environment.GetEnvironmentVariable("CHANGE_ME");

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {

                    await sqlConnection.OpenAsync();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sqlConnection;

                        //Your SQL query
                        command.CommandText = $"SELECT * FROM Bezoekers where DagVanDeWeek = @day";

                        //Adding parameters
                        command.Parameters.AddWithValue("@day", day);

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {

                            names.Add(reader["name"].ToString());
                            // string column1 = reader["name"].ToString();
                            // int column2 = int.Parse(reader["id"].ToString());
                            /*
                             *
                             * Will loop every row in within the result of the query
                             * Do whatevery you want here
                             * Create a list of objects, add data to it and return the data to the API
                             *
                             */
                        }
                    }

                }
                //Can also return the structure of a class
                return new OkObjectResult(names);
            }
            catch (Exception e)
            {
                //Returned http status code
                return new StatusCodeResult(500);
            }

        }
    }
}
