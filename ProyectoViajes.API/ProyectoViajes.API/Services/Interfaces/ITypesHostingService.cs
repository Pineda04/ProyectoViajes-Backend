using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface ITypesHostingService
    {
        Task<ResponseDto<List<TypeHostingDto>>> GetTypesHostingListAsync();
        Task<ResponseDto<TypeHostingDto>> GetTypeHostingByIdAsync(Guid id);
        Task<ResponseDto<TypeHostingDto>> CreateAsync(TypeHostingCreateDto dto);
        Task<ResponseDto<TypeHostingDto>> EditAsync(TypeHostingEditDto dto, Guid id);
        Task<ResponseDto<TypeHostingDto>> DeleteAsync(Guid id);
    }
}
