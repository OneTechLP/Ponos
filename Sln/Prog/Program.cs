using System;
using System.Linq;

namespace Prog
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir;

            if (args.Length > 0 && args[0] != null)
            {
                dir = args[0];
            }
            else
            {
                dir = "C:/data/";
            }
            
            var priceFilePath = $"{dir}prices.csv";

            //Read Product List From File
            var productList = Product.ReadPricesFromFile(priceFilePath).Result;
            Console.WriteLine($"Price List Read, There are {productList.Count()} Products in the List.");

            //Read Receipts From directory
            var productsFromReceipts = Product.ReadReceiptFromFile(dir).Result;
            Console.WriteLine($"Receipts in {dir} Directory Read, There are {productsFromReceipts.Count()} Non-Voided Products from the Receipt Files.");

            //Calculate Differences
            var differences = WrongPriceProduct.FindImproperlyPricedProducts(productList, productsFromReceipts).ToList();
            Console.WriteLine($"There were {differences.Count()} incorrectly priced products in the receipts.");

            //Build Report
            var reportInfo = WrongPriceProduct.BuildWrongProductPriceReport(differences);
            Console.WriteLine($"There are {reportInfo.Count()-1} different products that were sold at the wrong price.");

            var reportPath = WrongPriceProduct.WriteReportFile(dir, reportInfo.ToArray());
            Console.WriteLine($"Report File Written to {reportPath}");

        }

        
    }
}
