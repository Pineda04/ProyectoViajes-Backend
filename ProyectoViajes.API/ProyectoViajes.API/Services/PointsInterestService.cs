using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class PointsInterestService : IPointsInterestService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;

        public PointsInterestService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PointInterestDto>>> GetPuntosDeInteresListAsync()
        {
            var pointsEntity = await _context.PointsInterest.ToListAsync();

            var pointsDto = _mapper.Map<List<PointInterestDto>>(pointsEntity);

            return new ResponseDto<List<PointInterestDto>>{
              StatusCode = 200,
              Status = true,
              Message = MessagesConstants.RECORDS_FOUND,
              Data = pointsDto  
            };
        }

        public async Task<ResponseDto<PointInterestDto>> GetPuntoDeInteresByIdAsync(Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if(pointsEntity == null){
                return new ResponseDto<PointInterestDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>{
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> CreatePuntoDeInteresAsync(PointInterestCreateDto dto)
        {
            var pointsEntity = _mapper.Map<PointInterestEntity>(dto);

            _context.PointsInterest.Add(pointsEntity);

            await _context.SaveChangesAsync();

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>{
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> EditPuntoDeInteresAsync(PointInterestEditDto dto, Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if(pointsEntity == null){
                return new ResponseDto<PointInterestDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.UPDATE_ERROR
                };
            }

            _mapper.Map(dto, pointsEntity);

            _context.PointsInterest.Update(pointsEntity);

            await _context.SaveChangesAsync();

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>{
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> DeletePuntoDeInteresAsync(Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if(pointsEntity == null){
                return new ResponseDto<PointInterestDto>{
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.DELETE_ERROR
                };
            }

            _context.PointsInterest.Remove(pointsEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<PointInterestDto>{
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}