using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/types_hosting")]
    [ApiController]
    public class TypesHostingController : ControllerBase
    {
       private readonly ITypesHostingService _services;

        public TypesHostingController(ITypesHostingService services) 
        {
            this._services = services;
        }
        
        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> GetAll(
            string searchTerm = "",
            int page = 1
        )
        {
            var response = await _services.GetTypesHostingListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Get(Guid id)
        {
            var response = await _services.GetTypeHostingByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Create(TypeHostingCreateDto dto)
        {
            var response = await _services.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Edit(TypeHostingEditDto dto, Guid id)
        {
            var response = await _services.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Delete(Guid id)
        {
            var response = await _services.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
