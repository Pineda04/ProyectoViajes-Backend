using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<FlightDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        )
        {
            var response = await _flightsService.GetFlightsListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<FlightDto>>> Get(Guid id)
        {
            var response = await _flightsService.GetFlightByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<FlightDto>>> Create(FlightCreateDto dto)
        {
            var response = await _flightsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<FlightDto>>> Edit(FlightEditDto dto, Guid id)
        {
            var response = await _flightsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<FlightDto>>> Delete(Guid id)
        {
            var response = await _flightsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
