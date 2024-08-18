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
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(objList);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon Created Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {


            ResponseDTO? response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null)
            {
                CouponDTO? objList = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(objList);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO coupon)
        {


            ResponseDTO? response = await _couponService.DeleteCouponAsync(coupon.CouponId);
            if (response != null)
            {
                TempData["success"] = "Coupon Deleted Successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }
    }
}
