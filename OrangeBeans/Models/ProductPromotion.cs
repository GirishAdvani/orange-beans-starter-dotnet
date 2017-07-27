using System;

namespace OrangeBeans.Models
{
    public class ProductPromotion
    {
        public int ID {get; set;}
        public int ProductId {get; set;}
        public PromotionType PromotionType {get; set;}  
    }
}