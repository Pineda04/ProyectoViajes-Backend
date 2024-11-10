using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Dtos.TypesFlight;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class TypesFlightService : ITypesFlightService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public TypesFlightService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<TypeFlightDto>>>> GetTypesFlightListAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var typesFlightQuery = _context.TypesFlight.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                typesFlightQuery = typesFlightQuery.Where(t => t.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = await typesFlightQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var typesFlightEntities = await typesFlightQuery
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var typesFlightDtos = _mapper.Map<List<TypeFlightDto>>(typesFlightEntities);

            return new ResponseDto<PaginationDto<List<TypeFlightDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<TypeFlightDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = typesFlightDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<TypeFlightDto>> GetTypeFlightByIdAsync(Guid id)
        {
            var typeFlightEntity = await _context.TypesFlight.FirstOrDefaultAsync(t => t.Id == id);

            if (typeFlightEntity == null)
            {
                return new ResponseDto<TypeFlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var typeFlightDto = _mapper.Map<TypeFlightDto>(typeFlightEntity);

            return new ResponseDto<TypeFlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = typeFlightDto
            };
        }
        public async Task<ResponseDto<TypeFlightDto>> CreateAsync(TypeFlightCreateDto dto)
        {
            var typeFlightEntity = _mapper.Map<TypeFlightEntity>(dto);

            _context.TypesFlight.Add(typeFlightEntity);

            await _context.SaveChangesAsync();

            var typeFlightDto = _mapper.Map<TypeFlightDto>(typeFlightEntity);

            return new ResponseDto<TypeFlightDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = typeFlightDto
            };
        }
        public async Task<ResponseDto<TypeFlightDto>> EditAsync(TypeFlightEditDto dto, Guid id)
        {
            var typeFlightEntity = await _context.TypesFlight.FirstOrDefaultAsync(t => t.Id == id);

            if (typeFlightEntity == null)
            {
                return new ResponseDto<TypeFlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _mapper.Map<TypeFlightEditDto, TypeFlightEntity>(dto, typeFlightEntity);

            _context.TypesFlight.Update(typeFlightEntity);

            await _context.SaveChangesAsync();

            var typeFlightDto = _mapper.Map<TypeFlightDto>(typeFlightEntity);

            return new ResponseDto<TypeFlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = typeFlightDto
            };
        }
        public async Task<ResponseDto<TypeFlightDto>> DeleteAsync(Guid id)
        {
            var typeFlightEntity = await _context.TypesFlight.FirstOrDefaultAsync(t => t.Id == id);

            if (typeFlightEntity == null)
            {
                return new ResponseDto<TypeFlightDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.TypesFlight.Remove(typeFlightEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TypeFlightDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }

    }
}
