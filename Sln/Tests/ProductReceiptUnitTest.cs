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
            string[] values =
            {
                "Some Store",
                "Some Product    1520256466              25.52 D",
                "                                    TOTAL 25.52"
            };
            var expectedValue = new List<Product>
            {
                new ()
                {
                    Price = 25.52m,
                    Code = "1520256466"
                }
            };

            var actualValue = Product.ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void SimpleReceiptWith1Item()
        {
            string[] values = { };
            var expectedValue = new List<Product>();

        }
    }
}