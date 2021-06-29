using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Prog;

namespace Tests
{
    public class ProductReceiptUnitTest
    {
        [Fact]
        public void EmptyReceipt()
        {
            string[] values = { };
            var expectedValue = new List<Product>();

            var actualValue = Product.ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }
    }
}