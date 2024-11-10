using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/hostings")]
    public class HostingsController : ControllerBase
    {
        private readonly IHostingsService _hostingsService;

        public HostingsController(IHostingsService hostingsService)
        {
            this._hostingsService = hostingsService;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<HostingDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        )
        {
            var response = await _hostingsService.GetHostingsListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<HostingDto>>> Get(Guid id)
        {
            var response = await _hostingsService.GetHostingByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<HostingDto>>> Create(HostingCreateDto dto)
        {
            var response = await _hostingsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<HostingDto>>> Edit(HostingEditDto dto, Guid id)
        {
            var response = await _hostingsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<HostingDto>>> Delete(Guid id)
        {
            var response = await _hostingsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}

