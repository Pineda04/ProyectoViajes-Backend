using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Reservations;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IReservationsService
    {
        Task<ResponseDto<PaginationDto<List<ReservationDto>>>> GetAllReservationsAsync(
            string searchTerm = "", int page = 1
        );

        Task<ResponseDto<ReservationDto>> GetReservationByIdAsync(Guid id);

        Task<ResponseDto<ReservationDto>> CreateAsync(ReservationCreateDto dto);

        Task<ResponseDto<ReservationDto>> EditAsync(ReservationEditDto dto, Guid id);

        Task<ResponseDto<ReservationDto>> DeleteAsync(Guid id);

        Task<ResponseDto<PaginationDto<List<ReservationDto>>>> GetReservationsByUserIdAsync(
        string userId,
        string searchTerm = "",
        int page = 1);
    }
}