using System.Collections.Generic;
using System.Linq;

namespace Prog
{
    public class WrongPriceProduct
    {
        public Product Product {get;set;}
        public decimal PriceDifference {get;set;}

        public static IEnumerable<WrongPriceProduct> FindImproperlyPricedProducts(IEnumerable<Product> prices,
            IEnumerable<Product> receipts)
        {
            var result =
                from p in prices
                join r in receipts
                    on p.Code equals r.Code
                where p.Price != r.Price
                select new WrongPriceProduct
                {
                    Product = p,
                    PriceDifference = r.Price - p.Price
                };
            return result;
        }

        public static IEnumerable<string> BuildWrongProductPriceReport(IEnumerable<WrongPriceProduct> wrongPrices)
        {
            if (wrongPrices.Any())
            {
                var report = wrongPrices.GroupBy(p => p.Product.Code)
                    .Select(r => new {code = r.Key, count = r.Count(), sum = r.Sum(dif => dif.PriceDifference)})
                    .OrderBy(r => r.sum)
                    .Select(r => $"{r.code},{r.count},{r.sum}");

                return report.ToList().Prepend("Product Code,Count,Total");
            }
            
            return new List<string>();
        }

        public static string WriteReportFile(string directory, string[] reportData)
        {
            var fullPath = $"{directory}report.csv";

            System.IO.File.WriteAllLines(fullPath, reportData);

            return fullPath;
        }
    }
}