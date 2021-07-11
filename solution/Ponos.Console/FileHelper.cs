using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ponos.Console
{
    public class FileHelper : IFileHelper
    {

        public IEnumerable<string> GetFiles(string rootDirectory, Func<string, bool> directoryFilter, string filePattern)
        {
            foreach (string matchedFile in Directory.GetFiles(rootDirectory, filePattern, SearchOption.TopDirectoryOnly))
            {
                yield return matchedFile;
            }

            var matchedDirectories = Directory.GetDirectories(rootDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(directoryFilter);

            foreach (var dir in matchedDirectories)
            {
                foreach (var file in GetFiles(dir, directoryFilter, filePattern))
                {
                    yield return file;
                }
            }
        }

        public List<string> ReadFileContentAsLines(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }

        public Dictionary<long, double> ReadPricesFile(string pricesFile)
        {
            var pricesDictionary = new Dictionary<long, double>();
            using var reader = new StreamReader(pricesFile);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                pricesDictionary.Add(Convert.ToInt64(values[0]), Convert.ToDouble(values[1]));
            }

            return pricesDictionary;
        }

        public List<string> RemoveExtraSpaces(List<string> lines)
        {
            var resultList = new List<string>();
            foreach (var line in lines)
            {
                resultList.Add(Regex.Replace(line.Trim(), @"\s+", " "));
            }
            return resultList;
        }

        public void SaveReport(List<string> reportLines)
        {
            File.WriteAllLines("report.csv", reportLines.ToArray());
        }
    }
}
