﻿namespace Mongo.Services.CouponAPI.Models.DTOs
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
    }
}
