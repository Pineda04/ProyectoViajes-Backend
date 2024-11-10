using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/assessments")]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentsService _assessmentsService;

        public AssessmentsController(IAssessmentsService assessmentsService)
        {
            _assessmentsService = assessmentsService;            
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        ){
            var response = await _assessmentsService.GetAssessmentsListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> Get(Guid id){
            var response = await _assessmentsService.GetAssessmentByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear un destino
        [HttpPost]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> Create(AssessmentCreateDto dto){
            var response = await _assessmentsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar un destino
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> Edit(AssessmentEditDto dto, Guid id){
            var response = await _assessmentsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar un destino
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> Delete(Guid id){
            var response = await _assessmentsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}