using System;

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
            //Read Receipts From directory
            var productsFromReceipts = Product.ReadReceiptFromFile(dir).Result;
            //Calculate Differences

            //Write Report         
        }

        
    }
}
