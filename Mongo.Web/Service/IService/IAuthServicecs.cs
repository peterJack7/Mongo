using Mongo.Web.Models;
using Mongo.Web.Models;

namespace Mongo.Web.Service.IService
{
    public interface IAuthServicecs
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequest);
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequest);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequest);
    }

}
