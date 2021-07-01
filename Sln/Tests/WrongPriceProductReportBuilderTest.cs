using Xunit;
using System.Collections.Generic;
using FluentAssertions;
using Prog;

namespace Tests
{
    public class WrongPriceProductReportBuilderTest
    {
        [Fact]
        public void EmptyWrongPriceProductList()
        {
            var wrongPriceList = new List<WrongPriceProduct>();

            var expectedResult = System.Array.Empty<string>();

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SingleWrongPriceSimpleReport()
        {
            var wrongPriceList = new List<WrongPriceProduct>
            {
                new()
                {
                    PriceDifference = 10m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                }
            };

            var expectedResult = new List<string>
            {
                "Product Code,Count,Total",
                "5460609671,1,10"
            };

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void TwoDifferentWrongPriceSimpleReport()
        {
            var wrongPriceList = new List<WrongPriceProduct>
            {
                new()
                {
                    PriceDifference = -10m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = -11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460619671"
                    }
                }
            };

            var expectedResult = new List<string>
            {
                "Product Code,Count,Total",
                "5460619671,1,-11",
                "5460609671,1,-10"
            };

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void TwoSameWrongPriceSimpleReport()
        {
            var wrongPriceList = new List<WrongPriceProduct>
            {
                new()
                {
                    PriceDifference = 10m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                }
            };

            var expectedResult = new List<string>
            {
                "Product Code,Count,Total",
                "5460609671,2,21"
            };

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void FourSame4DifferentWrongPriceSimpleReport()
        {
            var wrongPriceList = new List<WrongPriceProduct>
            {
                new()
                {
                    PriceDifference = 10m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = -21m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "1460609671"
                    }
                },
                new()
                {
                    PriceDifference = -13m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "2460609671"
                    }
                },
                new()
                {
                    PriceDifference = -12m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "3460609671"
                    }
                },
                new()
                {
                    PriceDifference = -11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "4460609671"
                    }
                }
            };

            var expectedResult = new List<string>
            {
                "Product Code,Count,Total",
                "1460609671,1,-21",
                "2460609671,1,-13",
                "3460609671,1,-12",
                "4460609671,1,-11",
                "5460609671,4,43"                
            };

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void FourSame4OtherTo0WrongPriceSimpleReport()
        {
            var wrongPriceList = new List<WrongPriceProduct>
            {
                new()
                {
                    PriceDifference = 10m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "5460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "1460609671"
                    }
                },
                new()
                {
                    PriceDifference = -11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "1460609671"
                    }
                },
                new()
                {
                    PriceDifference = 11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "1460609671"
                    }
                },
                new()
                {
                    PriceDifference = -11m,
                    Product = new Product
                    {
                        Price = 22m,
                        Code = "1460609671"
                    }
                }
            };

            var expectedResult = new List<string>
            {
                "Product Code,Count,Total",
                "5460609671,4,43",
                "1460609671,4,0"
            };

            var report = WrongPriceProduct.BuildWrongProductPriceReport(wrongPriceList);

            report.Should().BeEquivalentTo(expectedResult);
        }
    }
}