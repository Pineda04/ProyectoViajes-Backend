using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Users;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseDto<PaginationDto<List<UserDto>>>> GetUsersListAsync(
           string searchTerm = "", int page = 1
        );
        Task<ResponseDto<UserDto>> GetUserByIdAsync(string id);
        Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, string id);
        Task<ResponseDto<UserDto>> DeleteAsync(string id);
    }
}
