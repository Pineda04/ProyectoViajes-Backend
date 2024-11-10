using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class DestinationsService : IDestinationsService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public DestinationsService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<DestinationDto>>>> GetDestinationsListAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var destinationsQuery = _context.Destinations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                destinationsQuery = destinationsQuery
                    .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())
                    || x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalDestinations = await destinationsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalDestinations / PAGE_SIZE);

            var destinationsEntity = await destinationsQuery
                .OrderByDescending(x => x.CreatedDate) 
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var destinationsDto = _mapper.Map<List<DestinationDto>>(destinationsEntity);

            return new ResponseDto<PaginationDto<List<DestinationDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Destinations found",
                Data = new PaginationDto<List<DestinationDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalDestinations,
                    TotalPages = totalPages,
                    Items = destinationsDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<DestinationDto>> GetDestinationByIdAsync(Guid id)
        {
            var destinationEntity = await _context.Destinations.Include(d => d.PointsInterest).FirstOrDefaultAsync(d => d.Id == id);

            if (destinationEntity == null)
            {
                return new ResponseDto<DestinationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var destinationDto = _mapper.Map<DestinationDto>(destinationEntity);

            return new ResponseDto<DestinationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = destinationDto
            };
        }

        public async Task<ResponseDto<DestinationDto>> CreateDestinationAsync(DestinationCreateDto dto)
        {
            var destinationEntity = _mapper.Map<DestinationEntity>(dto);

            _context.Destinations.Add(destinationEntity);

            await _context.SaveChangesAsync();

            var destinationDto = _mapper.Map<DestinationDto>(destinationEntity);

            return new ResponseDto<DestinationDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = destinationDto
            };

        }

        public async Task<ResponseDto<DestinationDto>> EditDestinationAsync(DestinationEditDto dto, Guid id)
        {
            var destinationEntity = await _context.Destinations.Include(d => d.PointsInterest).FirstOrDefaultAsync(d => d.Id == id);

            if (destinationEntity == null)
            {
                return new ResponseDto<DestinationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _mapper.Map(dto, destinationEntity);

            _context.Destinations.Update(destinationEntity);

            await _context.SaveChangesAsync();

            var destinationDto = _mapper.Map<DestinationDto>(destinationEntity);

            return new ResponseDto<DestinationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = destinationDto
            };
        }

        public async Task<ResponseDto<DestinationDto>> DeleteDestinationAsync(Guid id)
        {
            var destinationEntity = await _context.Destinations.FirstOrDefaultAsync(d => d.Id == id);

            if (destinationEntity == null)
            {
                return new ResponseDto<DestinationDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.Destinations.Remove(destinationEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<DestinationDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}