using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Dtos.TypesFlight;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/types_flight")]
    [ApiController]
    public class TypesFlightController : ControllerBase
    {
        private readonly ITypesFlightService _services;

        public TypesFlightController(ITypesFlightService services)
        {
            _services = services;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<TypeFlightDto>>> GetAll(
            string searchTerm = "",
            int page = 1
        )
        {
            var response = await _services.GetTypesFlightListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<TypeFlightDto>>> Get(Guid id)
        {
            var response = await _services.GetTypeFlightByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<TypeFlightDto>>> Create(TypeFlightCreateDto dto)
        {
            var response = await _services.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TypeFlightDto>>> Edit(TypeFlightEditDto dto, Guid id)
        {
            var response = await _services.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TypeFlightDto>>> Delete(Guid id)
        {
            var response = await _services.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
