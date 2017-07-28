using System;
using System.Collections.Generic;
using FluentAssertions;
using OrangeBeans.Models;
using Xunit;

namespace OrangeBeans.Tests
{
  public class PricingServiceTests
  {
    [Fact]
    public void OrderWithNoDiscountTest()
    {
      var service = new PricingService();
      Order order = new Order();
      var products = new Dictionary<Product, int>();
      products.Add(new Product { Name = "iPhone 6", Price = 25000 }, 1);   
      products.Add(new Product { Name = "iPhone 7", Price = 35000 }, 2);         
      order.Products = products;

      var orderTotal = service.GetFinalPrice(order);

      orderTotal.Should().Be(95000);
    }

    [Fact]
    public void OrderWithDiscountTest()
    {
      var service = new PricingService();
      //add discount for iPhone 7
      var currentDiscounts = new Dictionary<string,int>();
      currentDiscounts.Add("iPhone 7", 10);
      service.CurrentDiscounts = currentDiscounts;

      Order order = new Order();
      var products = new Dictionary<Product, int>();
      products.Add(new Product { Name = "iPhone 6", Price = 25000 }, 1);   
      products.Add(new Product { Name = "iPhone 7", Price = 35000 }, 2);         
      order.Products = products;
      
      var orderTotal = service.GetFinalPrice(order);

      orderTotal.Should().Be(88000);
    }

    [Fact]
    public void OrderWithBuyTwoGetOnePromoTest()
    {
      var service = new PricingService();
      
      //add buy 2 get 1 free promo
      List<ProductQuantityPromo> quantityPromo = new List<ProductQuantityPromo> 
      {
            new ProductQuantityPromo { ProductName = "Levi's Jeans", BoughtQuantity = 2, FreeQuantity = 1 }
      };
      service.QuantityPromos = quantityPromo;
      
      Order order = new Order();
      var products = new Dictionary<Product, int>();
      products.Add(new Product { Name = "Levi's Jeans", Price = 1500 }, 2);   
      products.Add(new Product { Name = "iPhone 7", Price = 35000 }, 2);         
      order.Products = products;
      
      var orderTotal = service.GetFinalPrice(order);

      orderTotal.Should().Be(71500);
    }

     [Fact]
    public void OrderWithBuyTwoGetOnePromoAndDiscountTest()
    {
      var service = new PricingService();

      //add buy 2 get 1 free promo
      List<ProductQuantityPromo> quantityPromo = new List<ProductQuantityPromo> 
      {
            new ProductQuantityPromo { ProductName = "Levi's Jeans", BoughtQuantity = 2, FreeQuantity = 1 }
      };
      service.QuantityPromos = quantityPromo;
      
      //add discount for iPhone 7
      var currentDiscounts = new Dictionary<string,int>();
      currentDiscounts.Add("iPhone 7", 10);
      service.CurrentDiscounts = currentDiscounts;

      Order order = new Order();
      var products = new Dictionary<Product, int>();
      products.Add(new Product { Name = "Levi's Jeans", Price = 1500 }, 2);   
      products.Add(new Product { Name = "iPhone 7", Price = 35000 }, 2);         
      order.Products = products;
      
      var orderTotal = service.GetFinalPrice(order);

      orderTotal.Should().Be(64500); //iPhone 7 : 63000 + Levi's jeans 1500
    }
  }
}
