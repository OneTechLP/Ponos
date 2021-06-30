﻿using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Prog;

namespace Tests
{
    public class WrongPriceProductProcessing
    {
        [Fact]
        public void EmptyPriceAndReceipts()
        {
            var prices = new List<Product>();
            var receipts = new List<Product>();

            var ExpectedValue = new List<WrongPriceProduct>();

            var PriceDifferences = WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);

            PriceDifferences.Should().BeEquivalentTo(ExpectedValue);
        }
        
        [Fact]
        public void PriceAndReceiptsMatch_EmptyReturn()
        {
            var prices = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 18.14m
                }
            };
            var receipts = prices;

            var ExpectedValue = new List<WrongPriceProduct>();

            var PriceDifferences = WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);

            PriceDifferences.Should().BeEquivalentTo(ExpectedValue);
        }

        [Fact]
        public void OneProductSoldForTooMuch()
        {
            var prices = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 18.14m
                }
            };
            var receipts = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 28.14m
                }
            };

            var ExpectedValue = new List<WrongPriceProduct>
            {
                new WrongPriceProduct()
                {
                    PriceDifference = 10m,
                    Product = new Product()
                    {
                        Code = "5258735248",
                        Price = 18.14m
                    }
                }
            };

            var PriceDifferences = WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);

            PriceDifferences.Should().BeEquivalentTo(ExpectedValue);
        }

        [Fact]
        public void OneProductSoldForNotEnough()
        {
            var prices = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 18.14m
                }
            };
            var receipts = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 8.14m
                }
            };

            var ExpectedValue = new List<WrongPriceProduct>
            {
                new WrongPriceProduct()
                {
                    PriceDifference = -10m,
                    Product = new Product()
                    {
                        Code = "5258735248",
                        Price = 18.14m
                    }
                }
            };

            var PriceDifferences = WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);

            PriceDifferences.Should().BeEquivalentTo(ExpectedValue);
        }

        [Fact]
        public void MultipleProductSoldForTooMuch()
        {
            var prices = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 18.14m
                }
            };
            var receipts = new List<Product>
            {
                new Product()
                {
                    Code = "5493535248",
                    Price = 10.87m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 28.14m
                },
                new Product()
                {
                    Code = "5258735248",
                    Price = 28.14m
                }
            };

            var ExpectedValue = new List<WrongPriceProduct>
            {
                new WrongPriceProduct()
                {
                    PriceDifference = 10m,
                    Product = new Product()
                    {
                        Code = "5258735248",
                        Price = 18.14m
                    }
                },
                new WrongPriceProduct()
                {
                    PriceDifference = 10m,
                    Product = new Product()
                    {
                        Code = "5258735248",
                        Price = 18.14m
                    }
                }
            };

            var PriceDifferences = WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);

            PriceDifferences.Should().BeEquivalentTo(ExpectedValue);
        }
    }
}