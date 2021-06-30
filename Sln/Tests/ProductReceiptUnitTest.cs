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

        }
        
        [Fact]
        public void SimpleReceiptWithNoItem()
        {
            string[] values =
            {
                "Some Store",
                "                                    TOTAL 25.52"
            };
            var expectedValue = new List<Product>();

            var actualValue = Product.ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void SimpleReceiptWith1Item()
        {
            string[] values =
            {
                "Some Store",
                "SOME ITEM      5493535248             10.87 F",
                "                                    TOTAL 25.52"
            };
            var expectedValue = new List<Product>
            {
                new ()
                {
                    Code = "5493535248",
                    Price = 10.87m
                }
            };

            var actualValue = Product.ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void SimpleReceiptWith2Item()
        {
            string[] values =
            {
                "Some Store",
                "SOME ITEM          5493535248             10.87 F",
                "SOME Other ITEM    5258735248             18.14 F",
                "                                    TOTAL 25.52"
            };
            var expectedValue = new List<Product>
            {
                new ()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new ()
                {
                    Code = "5258735248",
                    Price = 18.14m
                }
            };

            var actualValue = Product.ConvertReceiptToEnumerable(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }
    }
}