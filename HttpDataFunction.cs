using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blob;

namespace CP.Functions
{
    public static class HttpDataFunction
    {
        [FunctionName("HttpDataFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("http-data/{rand-guid}.json", FileAccess.Write, Connection = "faassamplestorage")] CloudBlockBlob outputBlob,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            await outputBlob.UploadTextAsync(requestBody);

            return new OkObjectResult("");
        }

    }
}
