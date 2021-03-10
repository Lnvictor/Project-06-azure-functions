using Microsoft.WindowsAzure.Storage.Table;

namespace models
{
     public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }
    }

    public class ProductTableEntity: TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }
    }

    public static class Mappings
    {

        public static ProductTableEntity ToTableEntity(this Product product)
        {
            return new ProductTableEntity
            {
                PartitionKey = "Product",
                RowKey = ""+ product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public static Product ToProduct(this ProductTableEntity product)
        {
            return new Product
            {
                Id = int.Parse(product.RowKey),
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }
    }
}