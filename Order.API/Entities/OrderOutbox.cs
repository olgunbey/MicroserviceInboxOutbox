﻿using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities
{
    public class OrderOutbox
    {
        [Key]
        public Guid IdempotentToken { get; set; }
        public int BuyerId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
