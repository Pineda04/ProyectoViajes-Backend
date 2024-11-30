using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TravelPackages;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface ITravelPackagesService
    {
        Task<ResponseDto<PaginationDto<List<TravelPackageDto>>>> GetTravelPackagesListAsync(
            string searchTerm = "", int page = 1, bool? isPopular = null, (double min, double max)? starRange = null
        );
        Task<ResponseDto<PaginationDto<List<TravelPackageDto>>>> GetTravelPackagesByDestinationAsync(
            Guid destinationId, int page = 1, string searchTerm = ""
        );
        Task<ResponseDto<TravelPackageDto>> GetTravelPackageByIdAsync(Guid id);
        Task<ResponseDto<TravelPackageDto>> CreateAsync(TravelPackageCreateDto dto);
        Task<ResponseDto<TravelPackageDto>> EditAsync(TravelPackageEditDto dto, Guid id);
        Task<ResponseDto<TravelPackageDto>> DeleteAsync(Guid id);
    }
}