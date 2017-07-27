using System;
using System.Collections.Generic;
using OrangeBeans.Models;

namespace OrangeBeans {

  public class PaymentService
  {
    PricingService pricingService = new PricingService();
    PaymentGateway gateway = new PaymentGateway();
    public string ProcessPayment(Order order)
    {
      float total = pricingService.GetFinalPrice(order);

      return "PR-" + gateway.InitiatePayment(total, order.ID);
    }
  }

}