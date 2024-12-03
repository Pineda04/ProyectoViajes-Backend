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

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> GetAll()
        {
            var response = await _assessmentsService.GetAllAssessmentsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<AssessmentDto>>> Get(Guid id)
        {
            var response = await _assessmentsService.GetAssessmentByIdAsync(id); return StatusCode(response.StatusCode, response);
        }

        [HttpGet("travel-package/{travelPackageId}")]
        public async Task<ActionResult<ResponseDto<List<AssessmentDto>>>> GetByTravelPackage(Guid travelPackageId)
        {
            var response = await _assessmentsService.GetAssessmentsByTravelPackageAsync(travelPackageId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<AssessmentDto>>> Create(AssessmentCreateDto dto)
        {
            var response = await _assessmentsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<AssessmentDto>>> Edit(AssessmentEditDto dto, Guid id)
        {
            var response = await _assessmentsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<AssessmentDto>>> Delete(Guid id)
        {
            var response = await _assessmentsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}