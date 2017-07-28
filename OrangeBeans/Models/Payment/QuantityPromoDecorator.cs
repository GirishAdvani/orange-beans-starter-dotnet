using System;
using System.Collections.Generic;
using System.Linq;

namespace OrangeBeans.Models
{
    public class QuantityPromoDecorator : PriceDecorator
    {
        private IPriceProcessor priceProcessor;
        public List<ProductQuantityPromo> QuantityPromos { get; set; }

        public QuantityPromoDecorator(IPriceProcessor priceProcessor)
        {
            this.priceProcessor = priceProcessor;
        }

        public override float CalculateOrderTotal(Dictionary<Product, int> products)
        {
            var modifiedProducts = new Dictionary<Product, int>();
            if(QuantityPromos != null && QuantityPromos.Count > 0)
            {
                ProductQuantityPromo quantityPromo = null;
                foreach(var product in products.Keys)
                {                    
                    quantityPromo = QuantityPromos.Where(p => p.ProductName == product.Name 
                        && p.BoughtQuantity == products[product]).SingleOrDefault();
                    modifiedProducts.Add(product, quantityPromo != null ? (products[product] - quantityPromo.FreeQuantity) : products[product]);                        
                }
            }
            else
            {
                modifiedProducts = products;
            }
            return priceProcessor.CalculateOrderTotal(modifiedProducts);
        }
    }

}