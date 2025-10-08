using AutenticaNET.API.DTOs;
using AutenticaNET.API.Models;

namespace AutenticaNET.API.Services.interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<User> GetByIdAsync(int id);
    }
}
