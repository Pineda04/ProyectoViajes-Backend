using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.PointsInterest;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IPointsInterestService
    {
        Task<ResponseDto<List<PointInterestDto>>> GetPuntosDeInteresListAsync();
        Task<ResponseDto<PointInterestDto>> GetPuntoDeInteresByIdAsync(Guid id);
        Task<ResponseDto<PointInterestDto>> CreatePuntoDeInteresAsync(PointInterestCreateDto dto);
        Task<ResponseDto<PointInterestDto>> EditPuntoDeInteresAsync(PointInterestEditDto dto, Guid id);
        Task<ResponseDto<PointInterestDto>> DeletePuntoDeInteresAsync(Guid id);
    }
}