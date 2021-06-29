using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Prog;

namespace Tests
{
    public class ProductPriceListUnitTest
    {
        public ProductPriceListUnitTest()
        {

        }

        [Fact]
        public void EmptyPrice()
        {
            string[] values = {  };
            var expectedValue = new List<Product>();

            var actualValue = BuildPriceList(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void SinglePrice()
        {
            string[] values = {"1984849, 12.25"};
            var expectedValue = new List<Product>
            {
                new()
                {
                    Code = "1984849",
                    Price = 12.25m
                }
            };

            var actualValue = BuildPriceList(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void PriceListOf2()
        {
            string[] values =
            {
                "1984849, 12.25",
                "2578957, 85.24"
            };

            var expectedValue = new List<Product>
            {
                new()
                {
                    Code = "1984849",
                    Price =12.25m
                },
                new ()
                {
                    Code = "2578957",
                    Price = 85.24m
                }
            };

            var actualValue = BuildPriceList(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void LargePriceList()
        {
            string[] values =
            {
                "1984849, 12.25",
                "2578957, 85.24",
                "2558557, 45.24",
                "2535477, 82.78",
                "5489957, 98.54",
                "3579857, 123.15",
                "2532189, 9875.45"
            };

            var expectedValue = BuildLargProductList();

            var actualValue = BuildPriceList(values);

            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        private List<Product> BuildPriceList(string[] values)
        {
            return Product.ConvertPricesToEnumberable(values).ToList();
        }

        private List<Product> BuildLargProductList()
        {
            var list = new List<Product>
            {
                new()
                {
                    Code = "1984849",
                    Price =12.25m
                },
                new ()
                {
                    Code = "2578957",
                    Price = 85.24m
                },
                new ()
                {
                    Code = "2558557",
                    Price = 45.24m
                },
                new ()
                {
                    Code = "2535477",
                    Price = 82.78m
                },
                new ()
                {
                    Code = "5489957",
                    Price = 98.54m
                },
                new ()
                {
                    Code = "3579857",
                    Price = 123.15m
                },
                new ()
                {
                    Code = "2532189",
                    Price = 9875.45m
                }
            };

            return list;
        }
    }
}
