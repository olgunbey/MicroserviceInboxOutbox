﻿namespace Order.API.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
