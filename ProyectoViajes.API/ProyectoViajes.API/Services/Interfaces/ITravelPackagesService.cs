using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TravelPackages;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface ITravelPackagesService
    {
        Task<ResponseDto<List<TravelPackageDto>>> GetTravelPackagesListAsync();
        Task<ResponseDto<TravelPackageDto>> GetTravelPackageByIdAsync(Guid id);
        Task<ResponseDto<TravelPackageDto>> CreateAsync(TravelPackageCreateDto dto);
        Task<ResponseDto<TravelPackageDto>> EditAsync(TravelPackageEditDto dto, Guid id);
        Task<ResponseDto<TravelPackageDto>> DeleteAsync(Guid id);
    }
}