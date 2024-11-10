using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesService _activitiesService;
        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ActivityDto>>>> GetAll(
            string searchTerm = "",
            int page = 1)
        {
            var response = await _activitiesService.GetActivitiesListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ActivityDto>>> Get(Guid id)
        {
            var response = await _activitiesService.GetActivityByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ActivityDto>>> Create(ActivityCreateDto dto)
        {
            var response = await _activitiesService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ActivityDto>>> Edit(ActivityEditDto dto, Guid id)
        {
            var response = await _activitiesService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }
        
        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ActivityDto>>> Delete(Guid id)
        {
            var response = await _activitiesService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}