using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService coupon)
        {

            _couponService = coupon;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? objList = new();

            ResponseDTO? response = await _couponService.GetAllCouponAsync();
            if (response != null)
            {
                objList = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            return View(objList);
        }
    }
}
