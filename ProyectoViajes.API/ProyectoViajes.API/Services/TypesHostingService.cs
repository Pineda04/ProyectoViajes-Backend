using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class TypesHostingService : ITypesHostingService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public TypesHostingService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<TypeHostingDto>>>> GetTypesHostingListAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var typesHostingQuery = _context.TypesHosting.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                typesHostingQuery = typesHostingQuery.Where(t => t.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = await typesHostingQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var typesHostingEntities = await typesHostingQuery
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var typesHostingDtos = _mapper.Map<List<TypeHostingDto>>(typesHostingEntities);

            return new ResponseDto<PaginationDto<List<TypeHostingDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<TypeHostingDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = typesHostingDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<TypeHostingDto>> GetTypeHostingByIdAsync(Guid id)
        {
            var typeHostingEntity = await _context.TypesHosting.FirstOrDefaultAsync(t => t.Id == id);

            if (typeHostingEntity == null)
            {
                return new ResponseDto<TypeHostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var typeHostingDto = _mapper.Map<TypeHostingDto>(typeHostingEntity);

            return new ResponseDto<TypeHostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = typeHostingDto
            };
        }
        public async Task<ResponseDto<TypeHostingDto>> CreateAsync(TypeHostingCreateDto dto)
        {
            var typeHostingEntity = _mapper.Map<TypeHostingEntity>(dto);

            _context.TypesHosting.Add(typeHostingEntity);

            await _context.SaveChangesAsync();

            var typeHostingDto = _mapper.Map<TypeHostingDto>(typeHostingEntity);

            return new ResponseDto<TypeHostingDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = typeHostingDto
            };
        }
        public async Task<ResponseDto<TypeHostingDto>> EditAsync(TypeHostingEditDto dto, Guid id)
        {
            var typeHostingEntity = await _context.TypesHosting.FirstOrDefaultAsync(t => t.Id == id);

            if (typeHostingEntity == null)
            {
                return new ResponseDto<TypeHostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _mapper.Map<TypeHostingEditDto, TypeHostingEntity>(dto, typeHostingEntity);

            _context.TypesHosting.Update(typeHostingEntity);

            await _context.SaveChangesAsync();

            var typeHostingDto = _mapper.Map<TypeHostingDto>(typeHostingEntity);

            return new ResponseDto<TypeHostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = typeHostingDto
            };
        }
        public async Task<ResponseDto<TypeHostingDto>> DeleteAsync(Guid id)
        {
            var typeHostingEntity = await _context.TypesHosting.FirstOrDefaultAsync(t => t.Id == id);

            if (typeHostingEntity == null)
            {
                return new ResponseDto<TypeHostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.TypesHosting.Remove(typeHostingEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TypeHostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }

    }
}
