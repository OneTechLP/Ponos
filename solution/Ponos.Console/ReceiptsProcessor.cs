using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponos.Console
{
    public class ReceiptsProcessor : IReceiptsProcessor
    {
        private readonly Dictionary<long, double> _pricesDictionary;
        private readonly List<Record> _recordsList;
        public ReceiptsProcessor(Dictionary<long, double> pricesDictionary)
        {
            _pricesDictionary = pricesDictionary;
            _recordsList = new List<Record>();
            foreach (var entry in pricesDictionary)
            {
                _recordsList.Add(new Record(entry.Key));
            }
        }
        public void CalculateMischarges(List<string> lines)
        {
            lines = RemoveExtraLines(lines);
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Split(' ').ToList();
                var productCode = GetProductCode(lines[i]);
                var productPrice = Convert.ToDouble(line[line.IndexOf(productCode) + 1]);
                var correctProductPrice = _pricesDictionary[Convert.ToInt64(productCode)];
                if (Convert.ToDouble(productPrice) != correctProductPrice)
                {
                    var found = _recordsList.FirstOrDefault(r => r.ProductCode == Convert.ToInt64(productCode));
                    found.IncreaseCount();
                    found.AddToTotal(Convert.ToDouble(productPrice) - correctProductPrice);
                }
            }
        }

        public List<string> GenerateReport()
        {
            var headerLine = $"Product Code,Count,Total";
            var resultList = new List<string>(){ headerLine};
            foreach (var rcd in _recordsList.OrderBy(r => r.Total))
            {
                resultList.Add(rcd.ToString());
            }

            return resultList;
        }

        public List<string> RemoveVoidedProductLines(List<string> lines)
        {
            var voidedProductIndices = lines.Select((item, index) => item.Contains("VOIDED PRODUCT") ? index : -1).Where(i => i != -1).ToList();
            foreach (var index in voidedProductIndices.OrderByDescending(i => i))
            {
                lines.RemoveAt(index - 1);
            }
            // Remove voided lines themslves
            voidedProductIndices = lines.Select((item, index) => item.Contains("VOIDED PRODUCT") ? index : -1).Where(i => i != -1).ToList();
            foreach (var index in voidedProductIndices.OrderByDescending(i => i))
            {
                lines.RemoveAt(index);
            }
            return lines;
        }

        private string GetProductCode(string line)
        {
            var ls = line.Trim().Split(' ')
                .ToList();
            return ls.First(l => l.Length == 10 && long.TryParse(l, out long val));
        }

        private List<string> RemoveExtraLines(List<string> lines)
        {
            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            return lines;
        }
    }
}
