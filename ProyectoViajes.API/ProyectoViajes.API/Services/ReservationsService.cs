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

        public ReservationsService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ReservationDto>>> GetAllReservationsAsync()
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
            .ToListAsync();

            var reservationDto = _mapper.Map<List<ReservationDto>>(reservationEntity);

            return new ResponseDto<List<ReservationDto>>{
              StatusCode = 200,
              Status = true,
              Message = MessagesConstants.RECORDS_FOUND,
              Data = reservationDto  
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

            if(reservationEntity is null){
                return new ResponseDto<ReservationDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>{
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

            return new ResponseDto<ReservationDto>{
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> EditAsync(ReservationEditDto dto, Guid id)
        {
            var reservationEntity = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if(reservationEntity is null){
                return new ResponseDto<ReservationDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.UPDATE_ERROR
                };
            }

            _mapper.Map(dto, reservationEntity);

            _context.Reservations.Update(reservationEntity);

            await _context.SaveChangesAsync();

            var reservationDto = _mapper.Map<ReservationDto>(reservationEntity);

            return new ResponseDto<ReservationDto>{
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = reservationDto
            };
        }

        public async Task<ResponseDto<ReservationDto>> DeleteAsync(Guid id)
        {
            var reservationEntity = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if(reservationEntity == null){
                return new ResponseDto<ReservationDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.DELETE_ERROR
                };
            }

            _context.Reservations.Remove(reservationEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<ReservationDto>{
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }      
    }
}