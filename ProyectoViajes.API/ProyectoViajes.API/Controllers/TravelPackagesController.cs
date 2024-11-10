using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TravelPackages;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/travel_packages")]
    public class TravelPackagesController : ControllerBase
    {
        private readonly ITravelPackagesService _travelPackagesService;
        public TravelPackagesController(ITravelPackagesService travelPackagesService)
        {
            _travelPackagesService = travelPackagesService;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<TravelPackageDto>>>> GetAll(
            string searchTerm = "",
            int page = 1,
            bool? isPopular = null
        )
        {
            var response = await _travelPackagesService.GetTravelPackagesListAsync(searchTerm, page, isPopular);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Get(Guid id)
        {
            var response = await _travelPackagesService.GetTravelPackageByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Create(TravelPackageCreateDto dto)
        {
            var response = await _travelPackagesService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Edit(TravelPackageEditDto dto, Guid id)
        {
            var response = await _travelPackagesService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TravelPackageDto>>> Delete(Guid id)
        {
            var response = await _travelPackagesService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}