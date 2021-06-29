

using System.Collections.Generic;
using Prog;
using Xunit;

namespace Tests
{
    public class ProductReceiptUnitTest
    {
        [Fact]
        public void EmptyReceipt()
        {
            string[] values = { };
            var expectedValue = new List<Product>();

            var actualValue = ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }
    }
}