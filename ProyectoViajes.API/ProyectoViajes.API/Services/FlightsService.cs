﻿using AutoMapper;
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
        
        // obtener listado de hospedajes 
        public async Task<ResponseDto<List<FlightDto>>> GetFlightsListAsync()
        {
            var flightEntities = await _context.Flights
                .Include(h => h.Destination)         
                .Include(h => h.TypeFlight)        
                .ToListAsync();

            var flightDtos = _mapper.Map<List<FlightDto>>(flightEntities);

            return new ResponseDto<List<FlightDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = flightDtos
            };
        }

         // obtener por id
        public async Task<ResponseDto<FlightDto>> GetFlightByIdAsync(Guid id)
        {
            var flightEntity = await _context.Flights
                .Include(h => h.Destination)       
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
                .Include(h => h.Destination)
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
            var destinationExists = await _context.Destinations.AnyAsync(d => d.Id == dto.DestinationId);
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