using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypesFlight;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface ITypesFlightService
    {
        Task<ResponseDto<List<TypeFlightDto>>> GetTypesFlightListAsync();
        Task<ResponseDto<TypeFlightDto>> GetTypeFlightByIdAsync(Guid id);
        Task<ResponseDto<TypeFlightDto>> CreateAsync(TypeFlightCreateDto dto);
        Task<ResponseDto<TypeFlightDto>> EditAsync(TypeFlightEditDto dto, Guid id);
        Task<ResponseDto<TypeFlightDto>> DeleteAsync(Guid id);
    }
}
