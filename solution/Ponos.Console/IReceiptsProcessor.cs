using System.Collections.Generic;

namespace Ponos.Console
{
    public interface IReceiptsProcessor
    {
        void CalculateMischarges(List<string> lines);
        List<string> RemoveVoidedProductLines(List<string> lines);
        List<string> GenerateReport();
    }
}
