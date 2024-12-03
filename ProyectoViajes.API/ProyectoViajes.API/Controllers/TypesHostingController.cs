using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/types_hosting")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TypesHostingController : ControllerBase
    {
       private readonly ITypesHostingService _services;

        public TypesHostingController(ITypesHostingService services) 
        {
            this._services = services;
        }
        
        // Traer todos
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Get(Guid id)
        {
            var response = await _services.GetTypeHostingByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Create(TypeHostingCreateDto dto)
        {
            var response = await _services.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Edit(TypeHostingEditDto dto, Guid id)
        {
            var response = await _services.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<TypeHostingDto>>> Delete(Guid id)
        {
            var response = await _services.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
