using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mongo.Web.Models;
using Mongo.Web.Service.IService;
using Mongo.Web.Utility;

namespace Mongo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServicecs _authService;

        public AuthController(IAuthServicecs authServicecs)
        {
            _authService = authServicecs;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO model = new();
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                  new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                  new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            ResponseDTO response = await _authService.RegisterAsync(model);
            ResponseDTO assignRole;

            if (response != null && response.IsSuccess)
            {
                model.Role = SD.RoleCustomer;
            }
            assignRole = await _authService.AssignRoleAsync(model);
            if (assignRole != null && assignRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";
                return RedirectToAction(nameof(Login));
            }
            var roleList = new List<SelectListItem>()
            {
                  new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                  new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
