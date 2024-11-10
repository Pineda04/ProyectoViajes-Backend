using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Reservations;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }
        // Traer todos
        [HttpGet]
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
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Get(Guid id)
        {
            var response = await _reservationsService.GetReservationByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        // Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Create(ReservationCreateDto dto)
        {
            var response = await _reservationsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        // Editar
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Edit(ReservationEditDto dto, Guid id)
        {
            var response = await _reservationsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }
        // Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Delete(Guid id)
        {
            var response = await _reservationsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}