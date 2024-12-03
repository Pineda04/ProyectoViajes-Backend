using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/points_interest")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PointsInterestController : ControllerBase
    {
        private readonly IPointsInterestService _pointsInterestService;
        public PointsInterestController(IPointsInterestService pointsInterestService)
        {
            _pointsInterestService = pointsInterestService;
        }

        // Traer todos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        ){
            var response = await _pointsInterestService.GetPointsInterestListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Get(Guid id){
            var response = await _pointsInterestService.GetPointInterestByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Crear un punto de interes
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Create(PointInterestCreateDto dto){
            var response = await _pointsInterestService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        // Editar un punto de interes
        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Edit(PointInterestEditDto dto ,Guid id){
            var response = await _pointsInterestService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar un punto de interes
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<List<PointInterestDto>>>> Delete(Guid id){
            var response = await _pointsInterestService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}