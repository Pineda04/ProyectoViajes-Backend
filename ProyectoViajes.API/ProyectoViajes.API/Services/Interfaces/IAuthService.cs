using ProyectoViajes.API.Dtos.Auth;
using ProyectoViajes.API.Dtos.Common;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);
    }
}