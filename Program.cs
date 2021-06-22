using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ponos
{
    class Product 
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
    }

    class PriceDifference 
    {
        public string Code { get; set; }
        public decimal Difference { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var prices = System.IO.File.ReadAllLines(@"./data/prices.csv")
                .Select(x => x.Split(','))
                .Select(x => new Product 
                {
                    Code = x[0],
                    Price = Convert.ToDecimal(x[1])
                })
                .ToList();

            var directory = args[0];

            // get all files
            var files = Directory.GetFiles(directory);
            
            // get all wrong charges
            var wrongChargesTasks = files.Select(i => GetWrongChargesAsync(i, prices));
            Task.WhenAll(wrongChargesTasks); 

            var wrongCharges = wrongChargesTasks
                .SelectMany(x => x.Result)
                .GroupBy(x => x.Code)
                .Select(x => new { key = x.Key, sum = x.Sum(y => y.Difference), count = x.Count()})
                .OrderByDescending(x => x.count)
                .ToArray();

            using(var w = new StreamWriter("./wrong-charges.csv"))
            {
                foreach(var wrongCharge in wrongCharges)
                {
                    var line = string.Format("{0},{1},{2}", wrongCharge.key, wrongCharge.count, wrongCharge.sum);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }

        static async Task<PriceDifference[]> GetWrongChargesAsync(string path, List<Product> prices)
        {
            var lines = (await System.IO.File.ReadAllLinesAsync(path))
                .Skip(1)      // remove store number
                .SkipLast(1); // remove total

            var regex = new Regex(@"\d+(\.\d+)?");

            return lines
                .Where(l => l.Contains("VOIDED") == false)
                .Select(x => regex.Matches(x).Select(y => y.Value))
                .Select(x => new Product 
                {
                    Code = x.ElementAt(0),
                    Price = Convert.ToDecimal(x.ElementAt(1))
                })
                .Where(x => prices.Any(y => y.Code == x.Code && y.Price != x.Price))
                .Join(
                    prices,
                    line => line.Code,
                    price => price.Code,
                    (line, price) => new PriceDifference { Code = line.Code, Difference = price.Price - line.Price }
                )
                .ToArray();
        }
    }
}
