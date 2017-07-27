using System;
using System.Collections.Generic;
using OrangeBeans.Models;

namespace OrangeBeans {

  public class PricingService
  {
        IPriceProcessor priceProcessor = new PriceProcessor();
        public Dictionary<string, int> CurrentDiscounts {get; set;}
        
        public float GetFinalPrice(Order order)
        {
            float originalTotal = priceProcessor.CalculateOrderTotal(order.Products);      
            DiscountPromoDecorator discountOffer = new DiscountPromoDecorator(priceProcessor);
            discountOffer.ProductDiscount = CurrentDiscounts;
            return discountOffer.CalculateOrderTotal(order.Products);
        }
  }
}