using System;
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
    public static class POST
    {

        /*
         *
         * ROUTES CAN BE CHANGED HOWEVER YOU WANT
         *
         * NOTE:
         * For a post, you will have to create a body conectent matching class
         *
         */



        /*
         *
         *  Post without a Database
         *
         */
        [FunctionName("POST")]
        public static async Task<IActionResult> POST_noDB(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "postroute")] HttpRequest req,
            ILogger log)
        {

            //Converting body to ExampleRequest
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ExampleRequest drinkRequest = JsonConvert.DeserializeObject<ExampleRequest>(requestBody);


            //Do stuff with your ExampleRequest instance.

            return new OkObjectResult("Cool_Data");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /*
         *
         *  Post with a Database
         *
         */
        [FunctionName("POST")]
        public static async Task<IActionResult> Post_DB(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "postroute")] HttpRequest req,
            ILogger log)
        {

            //Converting body to ExampleRequest
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ExampleRequest exampleRequest = JsonConvert.DeserializeObject<ExampleRequest>(requestBody);

            /*
            *
            * Dont forget to add your ConnectionString to local.settings.json and changee the name under here
            * Can be found in the Azure portal
            *
            */

            //Database shit
            string connection = Environment.GetEnvironmentVariable("CHANGE_ME");

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {

                await sqlConnection.OpenAsync();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = sqlConnection;

                    //Your SQL query
                    command.CommandText = $"INSERT INTO Bezoekers Values (@Name, @Address, @Age)";

                    command.Parameters.AddWithValue("@Name", exampleRequest.name);
                    command.Parameters.AddWithValue("@Address", exampleRequest.address);
                    command.Parameters.AddWithValue("@Age", exampleRequest.age);

                    //execute
                    await command.ExecuteNonQueryAsync();

                    //return object to API
                    return new OkObjectResult(exampleRequest);
                }

            }


            //Do stuff with your ExampleRequest instance.

            
        }
    }
}
