using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public FlightsService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<FlightDto>>>> GetFlightsListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var flightsQuery = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                flightsQuery = flightsQuery
                    .Where(f => f.Airline.ToLower().Contains(searchTerm.ToLower())
                    );
            }

            int totalFlights = await flightsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalFlights / PAGE_SIZE);

            var flightEntities = await flightsQuery
                .Include(f => f.TravelPackage)
                .Include(f => f.TypeFlight)
                .OrderByDescending(f => f.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var flightDtos = _mapper.Map<List<FlightDto>>(flightEntities);

            return new ResponseDto<PaginationDto<List<FlightDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<FlightDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalFlights,
                    TotalPages = totalPages,
                    Items = flightDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        // obtener por id
        public async Task<ResponseDto<FlightDto>> GetFlightByIdAsync(Guid id)
        {
            var flightEntity = await _context.Flights
                .Include(h => h.TravelPackage)
                .Include(h => h.TypeFlight)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (flightEntity == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var flightDto = _mapper.Map<FlightDto>(flightEntity);

            return new ResponseDto<FlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = flightDto
            };
        }

        // Crear un nuevo hospedaje  
        public async Task<ResponseDto<FlightDto>> CreateAsync(FlightCreateDto dto)
        {
            var flightEntity = _mapper.Map<FlightEntity>(dto);
            _context.Flights.Add(flightEntity);
            await _context.SaveChangesAsync();
            var flightDto = _mapper.Map<FlightDto>(flightEntity);
            return new ResponseDto<FlightDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = flightDto
            };
        }

        // Editar hospedaje existente
        public async Task<ResponseDto<FlightDto>> EditAsync(FlightEditDto dto, Guid id)
        {
            var flightEntity = await _context.Flights
                .Include(h => h.TravelPackage)
                .Include(h => h.TypeFlight)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (flightEntity == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            // Validar relaciones en caso de que el destino o tipo de hospedaje cambien
            var destinationExists = await _context.Destinations.AnyAsync(d => d.Id == dto.TravelPackageId);
            var typeFlightExists = await _context.TypesFlight.AnyAsync(th => th.Id == dto.TypeFlightId);

            if (!destinationExists || !typeFlightExists)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "no se encontro relacion"
                };
            }

            _mapper.Map(dto, flightEntity);
            _context.Flights.Update(flightEntity);
            await _context.SaveChangesAsync();

            var flightDto = _mapper.Map<FlightDto>(flightEntity);

            return new ResponseDto<FlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = flightDto
            };
        }

        // Eliminar hospedaje
        public async Task<ResponseDto<FlightDto>> DeleteAsync(Guid id)
        {
            var flightEntity = await _context.Flights.FirstOrDefaultAsync(h => h.Id == id);

            if (flightEntity == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.Flights.Remove(flightEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<FlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}
