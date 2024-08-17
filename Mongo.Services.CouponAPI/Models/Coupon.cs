using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mongo.Services.CouponAPI.Models
{
    public class Coupon
    {

        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get;set; }
        [Required]
        public double DiscountAmount { get; set; }

        public double MinAmount { get; set; }
    }
}
