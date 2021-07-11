using System;
using System.Collections.Generic;
namespace Ponos.Console
{
    public interface IFileHelper
    {
        IEnumerable<string> GetFiles(string rootDirectory, Func<string, bool> directoryFilter, string filePattern);
        List<string> RemoveExtraSpaces(List<string> lines);
        Dictionary<long, double> ReadPricesFile(string pricesFile);
        void SaveReport(List<string> reportLines);
        List<string> ReadFileContentAsLines(string filePath);
    }
}
