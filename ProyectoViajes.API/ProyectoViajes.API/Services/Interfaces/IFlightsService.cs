using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IFlightsService
    { 
        Task<ResponseDto<List<FlightDto>>> GetFlightsListAsync();
        Task<ResponseDto<FlightDto>> GetFlightByIdAsync(Guid id);
        Task<ResponseDto<FlightDto>> CreateAsync(FlightCreateDto dto);
        Task<ResponseDto<FlightDto>> EditAsync(FlightEditDto dto, Guid id);
        Task<ResponseDto<FlightDto>> DeleteAsync(Guid id);
    }
}
