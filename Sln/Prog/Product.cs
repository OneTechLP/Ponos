using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Prog
{
    public class Product
    {
        public string Code {get;set;}
        public decimal Price {get;set;}

        public static async Task<IEnumerable<Product>> ReadPricesFromFile(string path)
        {
            var fileContents = await ReadFromFile(path);

            return ConvertPricesToEnumberable(fileContents);
        }

        public static IEnumerable<Product> ConvertPricesToEnumberable(string[] values)
        {
            return values.Select(product => product.Split(','))
                .Select(product => new Product 
                {
                    Code = product[0],
                    Price = Convert.ToDecimal(product[1])
                });
        }

        public static async Task<IEnumerable<Product>> ReadReceiptFromFile(string path)
        {
            var fileContents = await ReadFromFile(path);

            return ConvertReceiptToEnumerable(fileContents);
        }

        public static IEnumerable<Product> ConvertReceiptToEnumerable(string[] values)
        {
            return new List<Product>();
        }

        private static async Task<string[]> ReadFromFile(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }
    }
}