using Mongo.Web.Models;

namespace Mongo.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetCouponAsync(string couponCode);
        Task<ResponseDTO?> GetAllCouponAsync();
        Task<ResponseDTO?> GetCouponByIdAsync(int couponCode);
        Task<ResponseDTO?> CreateCouponAsync(CouponDTO coupon);
        Task<ResponseDTO?> UpdateCouponAsync(CouponDTO coupon);
        Task<ResponseDTO?> DeleteCouponAsync(int couponCode);
    }
}
