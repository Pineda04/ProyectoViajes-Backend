using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/destinations")]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationsService _destinationsService;

        public DestinationsController(IDestinationsService destinationsService)
        {
            _destinationsService = destinationsService;            
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<DestinationDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        ){
            var response = await _destinationsService.GetDestinationsListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<DestinationDto>>>> Get(Guid id){
            var response = await _destinationsService.GetDestinationByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear un destino
        [HttpPost]
        public async Task<ActionResult<ResponseDto<List<DestinationDto>>>> Create(DestinationCreateDto dto){
            var response = await _destinationsService.CreateDestinationAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar un destino
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<DestinationDto>>>> Edit(DestinationEditDto dto, Guid id){
            var response = await _destinationsService.EditDestinationAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar un destino
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<List<DestinationDto>>>> Delete(Guid id){
            var response = await _destinationsService.DeleteDestinationAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}