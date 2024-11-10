using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/points_interest")]
    public class PointsInterestController : ControllerBase
    {
        private readonly IPointsInterestService _pointsInterestService;
        public PointsInterestController(IPointsInterestService pointsInterestService)
        {
            _pointsInterestService = pointsInterestService;
        }

        // Traer todos
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        ){
            var response = await _pointsInterestService.GetPointsInterestListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Get(Guid id){
            var response = await _pointsInterestService.GetPointInterestByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear un punto de interes
        [HttpPost]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Create(PointInterestCreateDto dto){
            var response = await _pointsInterestService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar un punto de interes
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Edit(PointInterestEditDto dto ,Guid id){
            var response = await _pointsInterestService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar un punto de interes
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Delete(Guid id){
            var response = await _pointsInterestService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}