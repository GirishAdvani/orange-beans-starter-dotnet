using System;
using System.Collections.Generic;

namespace OrangeBeans.Models
{
    public class DiscountPromoDecorator : PriceDecorator
    {
        private IPriceProcessor priceProcessor;
        public Dictionary<string, int> ProductDiscount { get; set; }

        public DiscountPromoDecorator(IPriceProcessor priceProcessor)
        {
            this.priceProcessor = priceProcessor;
            ProductDiscount = new Dictionary<string,int>();
        }

        public override float CalculateOrderTotal(Dictionary<Product, int> products)
        {
            if(ProductDiscount != null && ProductDiscount.Count > 0)
            {
                int productDiscount = 0; 
                foreach(var product in products.Keys)
                {
                    if(ProductDiscount.TryGetValue(product.Name, out productDiscount))
                    {
                        product.Price = product.Price - ((product.Price * productDiscount) / 100); 
                    }               
                }
            }
            return priceProcessor.CalculateOrderTotal(products);
        }
    }
}