using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Mongo.Services.AuthAPI.Data;
using Mongo.Services.AuthAPI.Models;
using Mongo.Services.AuthAPI.Models.DTOs;
using Mongo.Services.AuthAPI.Service.IService;

namespace Mongo.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO() { Token = "", User = null };
            }

            UserDTO userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumner = user.PhoneNumber
            };

            LoginResponseDTO response = new LoginResponseDTO()
            {
                Token = "",
                User = userDTO
            };
            return response;
        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.Email,
                Name = registrationRequestDTO.Name,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                PhoneNumber = registrationRequestDTO.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.Email);

                    if (userToReturn != null)
                    {
                        UserDTO userDTO = new()
                        {
                            Email = userToReturn.Email,
                            ID = userToReturn.Id,
                            Name = userToReturn.Name,
                            PhoneNumner = userToReturn.PhoneNumber
                        };
                        return "";
                    }
                    else
                    {
                        return result.Errors.FirstOrDefault().Description;
                    }
                }
            }
            catch (Exception ex) { }
            return "Error Occured";
        }
    }
}
