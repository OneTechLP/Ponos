namespace Ponos.Console
{
    public class Record
    {
        public long ProductCode { get; private set; }
        public int Count { get; private set; }
        public double Total { get; private set; }

        public Record(long productCode)
        {
            this.ProductCode = productCode;
        }

        public void IncreaseCount()
        {
            this.Count++;
        }

        public void AddToTotal(double toAdd)
        {
            this.Total += toAdd;
        }

        public override string ToString()
        {
            return $"{ProductCode},{Count},{Total:0.###}";
        }
    }
}
