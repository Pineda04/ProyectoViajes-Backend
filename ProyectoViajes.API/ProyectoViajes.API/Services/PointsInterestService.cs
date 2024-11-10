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
        private readonly int PAGE_SIZE;

        public PointsInterestService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<PointInterestDto>>>> GetPointsInterestListAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var pointsQuery = _context.PointsInterest.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                pointsQuery = pointsQuery
                    .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                p.Destination.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = await pointsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var pointEntities = await pointsQuery
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var pointDtos = _mapper.Map<List<PointInterestDto>>(pointEntities);

            return new ResponseDto<PaginationDto<List<PointInterestDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<PointInterestDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = pointDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }


        public async Task<ResponseDto<PointInterestDto>> GetPointInterestByIdAsync(Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if (pointsEntity == null)
            {
                return new ResponseDto<PointInterestDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> CreateAsync(PointInterestCreateDto dto)
        {
            var pointsEntity = _mapper.Map<PointInterestEntity>(dto);

            _context.PointsInterest.Add(pointsEntity);

            await _context.SaveChangesAsync();

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> EditAsync(PointInterestEditDto dto, Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if (pointsEntity == null)
            {
                return new ResponseDto<PointInterestDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.UPDATE_ERROR
                };
            }

            _mapper.Map(dto, pointsEntity);

            _context.PointsInterest.Update(pointsEntity);

            await _context.SaveChangesAsync();

            var pointsDto = _mapper.Map<PointInterestDto>(pointsEntity);

            return new ResponseDto<PointInterestDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = pointsDto
            };
        }

        public async Task<ResponseDto<PointInterestDto>> DeleteAsync(Guid id)
        {
            var pointsEntity = await _context.PointsInterest.FirstOrDefaultAsync(p => p.Id == id);

            if (pointsEntity == null)
            {
                return new ResponseDto<PointInterestDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.DELETE_ERROR
                };
            }

            _context.PointsInterest.Remove(pointsEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<PointInterestDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}