using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Reservations;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IReservationsService
    {
        Task<ResponseDto<List<ReservationDto>>> GetAllReservationsAsync();
        Task<ResponseDto<ReservationDto>> GetReservationByIdAsync(Guid id);
        Task<ResponseDto<List<ReservationDto>>> GetReservationsByUserAsync(string userId);
        Task<ResponseDto<List<ReservationDto>>> GetReservationsByTravelPackageAsync(Guid travelPackageId);
        Task<ResponseDto<ReservationDto>> CreateAsync(ReservationCreateDto dto);
        Task<ResponseDto<ReservationDto>> EditAsync(ReservationEditDto dto, Guid id);
        Task<ResponseDto<ReservationDto>> DeleteAsync(Guid id);
    }
}