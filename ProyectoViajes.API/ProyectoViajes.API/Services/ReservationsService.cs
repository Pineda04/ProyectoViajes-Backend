using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
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
        private readonly int PAGE_SIZE;

        public ReservationsService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<ReservationDto>>>> GetAllReservationsAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var reservationsQuery = _context.Reservations
                .Include(r => r.TravelPackage)
                .Include(r => r.Flight).ThenInclude(f => f.TravelPackage)  
                .Include(r => r.Hosting).ThenInclude(h => h.TravelPackage)  
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reservationsQuery = reservationsQuery
                    .Where(r => r.TravelPackage.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                r.Flight.Airline.ToLower().Contains(searchTerm.ToLower()) ||
                                r.Hosting.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                r.Flight.TravelPackage.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = await reservationsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var reservationEntities = await reservationsQuery
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    TravelPackageId = r.TravelPackageId,
                    TravelPackageName = r.TravelPackage.Name,
                    FlightId = r.FlightId,
                    FlightAirline = r.Flight.Airline,
                    HostingId = r.HostingId,
                    HostingName = r.Hosting.Name,
                    ReservationDate = r.ReservationDate,
                    UserId = r.UserId
                })
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservationEntities);

            return new ResponseDto<PaginationDto<List<ReservationDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<ReservationDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = reservationDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<ReservationDto>> GetReservationByIdAsync(Guid id)
        {
            var reservationEntity = await _context.Reservations
            .Include(r => r.TravelPackage)
                .Include(r => r.Flight)
                .Include(r => r.Hosting)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    TravelPackageId = r.TravelPackageId,
                    TravelPackageName = r.TravelPackage.Name,
                    FlightId = r.FlightId,
                    FlightAirline = r.Flight.Airline,
                    HostingId = r.HostingId,
                    HostingName = r.Hosting.Name,
                    ReservationDate = r.ReservationDate,
                    UserId = r.UserId
                })
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> CreateAsync(ReservationCreateDto dto)
        {
            var reservationEntity = _mapper.Map<ReservationEntity>(dto);

            _context.Reservations.Add(reservationEntity);

            await _context.SaveChangesAsync();

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
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
                    Message = MessagesConstants.UPDATE_ERROR
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
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> DeleteAsync(Guid id)
        {
            var reservationEntity = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity == null)
            {
                return new ResponseDto<ReservationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.DELETE_ERROR
                };
            }

            _context.Reservations.Remove(reservationEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<ReservationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}