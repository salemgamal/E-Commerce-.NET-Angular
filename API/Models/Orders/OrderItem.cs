﻿using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Products;

namespace API.Models.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
