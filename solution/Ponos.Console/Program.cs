using System;
using System.IO;
using System.Linq;

namespace Ponos.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length <= 0)
            {
                System.Console.Out.WriteLine("receipts directory not specified, please try again with a valid directory.");
                return;
            }
            if (!Directory.Exists(args[0]))
            {
                System.Console.Out.WriteLine($"Specified directory: {args[0]} not found , please try again with a valid directory.");
                return;
            }
            try
            {
                var fileHelper = new FileHelper();
                var pricedDic = fileHelper.ReadPricesFile("prices.csv");
                var receiptsProcessor = new ReceiptsProcessor(pricedDic);
                var files = fileHelper.GetFiles(args[0], d => !d.Contains("Pax", StringComparison.OrdinalIgnoreCase), "*.*").ToList();
                System.Console.Out.WriteLine($"About to process {files.Count} files!");
                foreach (var file in files)
                {
                    var lines = fileHelper.ReadFileContentAsLines(file);
                    lines = fileHelper.RemoveExtraSpaces(lines);
                    lines = receiptsProcessor.RemoveVoidedProductLines(lines);
                    receiptsProcessor.CalculateMischarges(lines);

                }
                var reportLines = receiptsProcessor.GenerateReport();
                fileHelper.SaveReport(reportLines);
                System.Console.Out.WriteLine("Finished processing and file report.csv was generated!");
            }
            catch (Exception ex)
            {
                System.Console.Out.WriteLine($"Something won't wrong please review the exception message: {ex.Message}");
            }
        }
    }
}
