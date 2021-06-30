using System.Collections.Generic;
using System.Linq;

namespace Prog
{
    public class WrongPriceProduct
    {
        public Product Product {get;set;}
        public decimal PriceDifference {get;set;}

        public static IEnumerable<WrongPriceProduct> FindImproperlyPricedProducts(IEnumerable<Product> prices,
            IEnumerable<Product> receipts)
        {
            var result =
                from p in prices
                join r in receipts
                    on p.Code equals r.Code
                where p.Price != r.Price
                select new WrongPriceProduct
                {
                    Product = p,
                    PriceDifference = r.Price - p.Price
                };
            return result;
        }
    }
}