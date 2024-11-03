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

        public FlightsService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<FlightDto>>> GetFlightsListAsync()
        {
            var flights = await _context.Flights
                .Include(f => f.TypeFlight)
                .Include(f => f.Destination)
                .ToListAsync();

            var flightDtos = _mapper.Map<List<FlightDto>>(flights);

            return new ResponseDto<List<FlightDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = flightDtos
            };
        }

        public async Task<ResponseDto<FlightDto>> GetFlightByIdAsync(Guid id)
        {
            var flight = await _context.Flights
                .Include(f => f.TypeFlight)
                .Include(f => f.Destination)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var flightDto = _mapper.Map<FlightDto>(flight);

            return new ResponseDto<FlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = flightDto
            };
        }

        public async Task<ResponseDto<FlightDto>> CreateAsync(FlightCreateDto dto)
        {
            // Validar si el tipo de vuelo existe
            var flightType = await _context.TypesFlight.FindAsync(Guid.Parse(dto.TypeFlightId));
            if (flightType == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Tipo de vuelo no encontrado."
                };
            }

            // Validar si el destino existe
            var destination = await _context.Destinations.FindAsync(Guid.Parse(dto.DestinationId));
            if (destination == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Destino no encontrado."
                };
            }

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

        public async Task<ResponseDto<FlightDto>> EditAsync(FlightEditDto dto, Guid id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            // Validar si el nuevo tipo de vuelo y destino existen
            if (dto.TypeFlightId != null)
            {
                var flightType = await _context.TypesFlight.FindAsync(Guid.Parse(dto.TypeFlightId));
                if (flightType == null)
                {
                    return new ResponseDto<FlightDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Tipo de vuelo no encontrado."
                    };
                }
            }

            if (dto.DestinationId != null)
            {
                var destination = await _context.Destinations.FindAsync(Guid.Parse(dto.DestinationId));
                if (destination == null)
                {
                    return new ResponseDto<FlightDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Destino no encontrado."
                    };
                }
            }

            // Actualizar campos
            _mapper.Map(dto, flight);
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();

            var flightDto = _mapper.Map<FlightDto>(flight);

            return new ResponseDto<FlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = flightDto
            };
        }

        public async Task<ResponseDto<FlightDto>> DeleteAsync(Guid id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return new ResponseDto<FlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.Flights.Remove(flight);
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
