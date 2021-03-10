using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using models;
using Newtonsoft.Json;

namespace ProductAPI
{
    public static class products
    {
        static List<Product> p = new List<Product>();

        [FunctionName("products")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("products", Connection = "AzureWebJobsStorage")] IAsyncCollector<ProductTableEntity> ProductTable, 
            ILogger log)
        {

            if (req.Method == "POST")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<Product>(requestBody);
                var prod = new Product{
                    Id = p.Count,
                    Name = input.Name,
                    Description = input.Description,
                    Value = input.Value
                };

                await ProductTable.AddAsync(prod.ToTableEntity());
                return new OkObjectResult(prod);
            }
            
            // var query = new TableQuery<ProductTableEntity>();
            // var segment = await productTable.ExecuteQuerySegmentedAsync<ProductTableEntity>(query, null);
            // return new OkObjectResult(segment.Select(Mappings.ToProduct));

            return new OkResult();
            
        }
    }
}

