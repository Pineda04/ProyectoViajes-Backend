using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Reservations;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;

        public ReservationsService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ReservationDto>>> GetAllReservationsAsync()
        {
            var reservationEntities = await _context.Reservations
                .Include(r => r.TravelPackage)
                .Include(r => r.Flight)
                .Include(r => r.Hosting)
                .Include(r => r.User)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservationEntities);

            return new ResponseDto<List<ReservationDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Reservaciones obtenida correctamente.",
                Data = reservationDtos
            };
        }

        public async Task<ResponseDto<ReservationDto>> GetReservationByIdAsync(Guid id)
        {
            var reservationEntity = await _context.Reservations
                .Include(r => r.TravelPackage)
                .Include(r => r.Flight)
                .Include(r => r.Hosting)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Reservación."
                };
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Reservación encontrada.",
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<List<ReservationDto>>> GetReservationsByUserAsync(string userId)
        {
            var reservationEntities = await _context.Reservations
                .Include(r => r.TravelPackage)
                .Include(r => r.Flight)
                .Include(r => r.Hosting)
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservationEntities);

            return new ResponseDto<List<ReservationDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Reservaciones del Usuario obtenida correctamente.",
                Data = reservationDtos
            };
        }

        public async Task<ResponseDto<List<ReservationDto>>> GetReservationsByTravelPackageAsync(Guid travelPackageId)
        {
            var reservationEntities = await _context.Reservations
                .Include(r => r.TravelPackage)
                .Include(r => r.Flight)
                .Include(r => r.Hosting)
                .Include(r => r.User)
                .Where(r => r.TravelPackageId == travelPackageId)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservationEntities);

            return new ResponseDto<List<ReservationDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Reservaciones del Paquete de Viaje obtenida correctamente.",
                Data = reservationDtos
            };
        }

        public async Task<ResponseDto<ReservationDto>> CreateAsync(ReservationCreateDto dto)
        {
            // Validar que el paquete de viaje, vuelo y alojamiento existan
            var travelPackageExists = await _context.TravelPackages.AnyAsync(tp => tp.Id == dto.TravelPackageId);
            var flightExists = await _context.Flights.AnyAsync(f => f.Id == dto.FlightId);
            var hostingExists = await _context.Hostings.AnyAsync(h => h.Id == dto.HostingId);
            // var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!travelPackageExists || !flightExists || !hostingExists) // Agregar || !userExists
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Algunos de los recursos para la reservación no existen."
                };
            }

            var reservationEntity = _mapper.Map<ReservationEntity>(dto);
            reservationEntity.ReservationDate = DateTime.Now;

            _context.Reservations.Add(reservationEntity);
            await _context.SaveChangesAsync();

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Reservación creada correctamente.",
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> EditAsync(ReservationEditDto dto, Guid id)
        {
            var reservationEntity = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Reservación."
                };
            }

            // Validar que el paquete de viaje, vuelo y alojamiento existan
            var travelPackageExists = await _context.TravelPackages.AnyAsync(tp => tp.Id == dto.TravelPackageId);
            var flightExists = await _context.Flights.AnyAsync(f => f.Id == dto.FlightId);
            var hostingExists = await _context.Hostings.AnyAsync(h => h.Id == dto.HostingId);
            // var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!travelPackageExists || !flightExists || !hostingExists) // Agregar  || !userExists
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Algunos de los recursos para la reservación no existen."
                };
            }

            _mapper.Map(dto, reservationEntity);
            _context.Reservations.Update(reservationEntity);
            await _context.SaveChangesAsync();

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Reservación editada correctamente.",
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> DeleteAsync(Guid id)
        {
            var reservationEntity = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Reservación."
                };
            }

            _context.Reservations.Remove(reservationEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Reservación eliminada correctamente."
            };
        }
    }
}