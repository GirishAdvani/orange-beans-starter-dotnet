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
      var currentDiscounts = new Dictionary<string,int>();
      currentDiscounts.Add("iPhone 7", 10);

      Order order = new Order();
      var products = new Dictionary<Product, int>();
      products.Add(new Product { Name = "iPhone 6", Price = 25000 }, 1);   
      products.Add(new Product { Name = "iPhone 7", Price = 35000 }, 2);         
      order.Products = products;
      
      var orderTotal = service.GetFinalPrice(order);

      orderTotal.Should().Be(95000);
    }
  }
}
