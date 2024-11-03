using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Common;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IActivitiesService
    {
        Task<ResponseDto<List<ActivityDto>>> GetActivitiesListAsync();
        Task<ResponseDto<ActivityDto>> GetActivityByIdAsync(Guid id);
        Task<ResponseDto<ActivityDto>> CreateAsync(ActivityCreateDto dto);
        Task<ResponseDto<ActivityDto>> EditAsync(ActivityEditDto dto, Guid id);
        Task<ResponseDto<ActivityDto>> DeleteAsync(Guid id);
    }
}