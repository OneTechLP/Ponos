using System;
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

            WrongPriceProduct.FindImproperlyPricedProducts(prices, receipts);
        }
    }
}