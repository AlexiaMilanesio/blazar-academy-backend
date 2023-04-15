using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Xml.Linq;


namespace FunctionAPI
{
    public static class FunctionAPI 
    {   
        [FunctionName("FunctionAPI")]
        //[Function("")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"]; // qui si legge la informazione passata in query string
            string lastname = req.Query["lastname"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); // qui si legge la informazione passata per il body
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            lastname = lastname ?? data?.lastname;

            string responseMessage = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname)
                ? "Error! Name and last name cannot be empty nor null."
                : $"Hello, {name} {lastname}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage); // questa funzione costruisce un oggetto di tipo ok object result 
        }
    }
}

