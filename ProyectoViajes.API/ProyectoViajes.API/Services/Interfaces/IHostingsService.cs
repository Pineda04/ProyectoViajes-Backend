using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IHostingsService
    {
        Task<ResponseDto<PaginationDto<List<HostingDto>>>> GetHostingsListAsync(
            string searchTerm = "", int page = 1
        );
        Task<ResponseDto<HostingDto>> GetHostingByIdAsync(Guid id);
        Task<ResponseDto<HostingDto>> CreateAsync(HostingCreateDto dto);
        Task<ResponseDto<HostingDto>> EditAsync(HostingEditDto dto, Guid id);
        Task<ResponseDto<HostingDto>> DeleteAsync(Guid id);
    }
}
