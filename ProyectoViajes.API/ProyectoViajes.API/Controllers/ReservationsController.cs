using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Reservations;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }
        // Traer todos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<ReservationDto>>>> GetAll(
            string searchTerm = "",
            int page = 1
        )
        {
            var response = await _reservationsService.GetAllReservationsAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }
        // Traer por id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Get(Guid id)
        {
            var response = await _reservationsService.GetReservationByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        // Crear
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Create(ReservationCreateDto dto)
        {
            var response = await _reservationsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        // Editar
        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Edit(ReservationEditDto dto, Guid id)
        {
            var response = await _reservationsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }
        // Eliminar
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Delete(Guid id)
        {
            var response = await _reservationsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}