using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TravelPackages;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/travel_packages")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TravelPackagesController : ControllerBase
    {
        private readonly ITravelPackagesService _travelPackagesService;
        public TravelPackagesController(ITravelPackagesService travelPackagesService)
        {
            _travelPackagesService = travelPackagesService;
        }

        // Traer todos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<TravelPackageDto>>>> GetAll(
            string searchTerm = "",
            int page = 1,
            bool? isPopular = null,
            (double min, double max)? starRange = null
        )
        {
            var response = await _travelPackagesService.GetTravelPackagesListAsync(searchTerm, page, isPopular, starRange);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Get(Guid id)
        {
            var response = await _travelPackagesService.GetTravelPackageByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Create(TravelPackageCreateDto dto)
        {
            var response = await _travelPackagesService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Edit(TravelPackageEditDto dto, Guid id)
        {
            var response = await _travelPackagesService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Obtener paquetes de viaje por destino
        [HttpGet("destination/{destinationId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<TravelPackageDto>>>>> GetByDestination(
            Guid destinationId,
            int page = 1,
            string searchTerm = ""
        )
        {
            var response = await _travelPackagesService.GetTravelPackagesByDestinationAsync(
                destinationId,
                page,
                searchTerm
            );
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Delete(Guid id)
        {
            var response = await _travelPackagesService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}