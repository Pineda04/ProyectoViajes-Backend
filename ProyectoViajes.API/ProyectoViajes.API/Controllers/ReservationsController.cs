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

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ReservationDto>>>> GetAll()
        {
            var response = await _reservationsService.GetAllReservationsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Get(Guid id)
        {
            var response = await _reservationsService.GetReservationByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ResponseDto<List<ReservationDto>>>> GetByUser (string userId)
        {
            var response = await _reservationsService.GetReservationsByUserAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("travel-package/{travelPackageId}")]
        public async Task<ActionResult<ResponseDto<List<ReservationDto>>>> GetByTravelPackage(Guid travelPackageId)
        {
            var response = await _reservationsService.GetReservationsByTravelPackageAsync(travelPackageId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Create(ReservationCreateDto dto)
        {
            var response = await _reservationsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Edit(ReservationEditDto dto, Guid id)
        {
            var response = await _reservationsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ReservationDto>>> Delete(Guid id)
        {
            var response = await _reservationsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}