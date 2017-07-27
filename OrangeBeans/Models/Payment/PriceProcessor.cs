using System;
using System.Collections.Generic;

namespace OrangeBeans.Models
{
    public interface IPriceProcessor
    {
        float CalculateOrderTotal(Dictionary<Product, int> Products);
    }

    public class PriceProcessor : IPriceProcessor
    {
        public float CalculateOrderTotal(Dictionary<Product, int> products)
        {
            float total = 0;

            foreach(var product in products.Keys){
                var quantity = products[product];
                total += product.Price * quantity;
            }
            return total;
        }
    }

    public abstract class PriceDecorator : IPriceProcessor
    {
        public abstract float CalculateOrderTotal(Dictionary<Product, int> Products);
    }
}