using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.TravelPackages;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class TravelPackagesService : ITravelPackagesService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public TravelPackagesService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<TravelPackageDto>>>> GetTravelPackagesListAsync(
            string searchTerm = "",
            int page = 1,
            bool? isPopular = null,
            (double min, double max)? starRange = null
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var travelPackagesQuery = _context.Travels
                .Include(tp => tp.Activities)
                .Include(tp => tp.Assessments)
                .AsQueryable();

            // Filtrar por término de búsqueda
            if (!string.IsNullOrEmpty(searchTerm))
            {
                travelPackagesQuery = travelPackagesQuery
                    .Where(tp => tp.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                 tp.Description.ToLower().Contains(searchTerm.ToLower()) ||
                                 tp.Assessments.Any(a => a.Comment.ToLower().Contains(searchTerm.ToLower()))
                    );
            }

            // Filtrar por rango de estrellas
            if (starRange.HasValue)
            {
                travelPackagesQuery = travelPackagesQuery
                    .Where(tp => tp.Assessments.Any() &&
                                 tp.Assessments.Average(a => a.Stars) >= starRange.Value.min &&
                                 tp.Assessments.Average(a => a.Stars) <= starRange.Value.max);
            }

            // Filtrar por paquetes populares
            if (isPopular.HasValue && isPopular.Value)
            {
                var popularPackages = await _context.Reservations
                    .GroupBy(r => r.TravelPackageId)
                    .Where(g => g.Count() > 3)
                    .Select(g => g.Key)
                    .ToListAsync();

                travelPackagesQuery = travelPackagesQuery
                    .Where(tp => popularPackages.Contains(tp.Id));
            }

            int totalItems = await travelPackagesQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var travelPackageEntities = await travelPackagesQuery
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var travelPackagesDtos = travelPackageEntities.Select(tp =>
            {
                // Calcular el promedio de estrellas 
                var averageStarsValue = tp.Assessments.Any() ? tp.Assessments.Average(a => a.Stars) : 0;
                var travelPackageDto = _mapper.Map<TravelPackageDto>(tp);

                // Asignar el promedio de estrellas al DTO
                travelPackageDto.AverageStars = averageStarsValue;

                travelPackageDto.Price = tp.Price;

                return travelPackageDto;
            }).ToList();

            return new ResponseDto<PaginationDto<List<TravelPackageDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<TravelPackageDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = travelPackagesDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<TravelPackageDto>> GetTravelPackageByIdAsync(Guid id)
        {
            var travelPackageEntity = await _context.Travels
                .Include(tp => tp.Activities).Include(tp => tp.Assessments).FirstOrDefaultAsync(tp => tp.Id == id);
            if (travelPackageEntity == null)
            {
                return new ResponseDto<TravelPackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            var travelPackageDto = _mapper.Map<TravelPackageDto>(travelPackageEntity);
            return new ResponseDto<TravelPackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = travelPackageDto
            };
        }

        public async Task<ResponseDto<TravelPackageDto>> CreateAsync(TravelPackageCreateDto dto)
        {
            var travelPackageEntity = _mapper.Map<TravelPackageEntity>(dto);
            _context.Travels.Add(travelPackageEntity);
            await _context.SaveChangesAsync();
            var travelPackageDto = _mapper.Map<TravelPackageDto>(travelPackageEntity);
            return new ResponseDto<TravelPackageDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = travelPackageDto
            };
        }

        public async Task<ResponseDto<TravelPackageDto>> EditAsync(TravelPackageEditDto dto, Guid id)
        {
            var travelPackageEntity = await _context.Travels
                .FirstOrDefaultAsync(tp => tp.Id == id);
            if (travelPackageEntity == null)
            {
                return new ResponseDto<TravelPackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _mapper.Map(dto, travelPackageEntity);
            _context.Travels.Update(travelPackageEntity);
            await _context.SaveChangesAsync();
            var travelPackageDto = _mapper.Map<TravelPackageDto>(travelPackageEntity);
            return new ResponseDto<TravelPackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = travelPackageDto
            };
        }
        public async Task<ResponseDto<TravelPackageDto>> DeleteAsync(Guid id)
        {
            var travelPackageEntity = await _context.Travels
                .FirstOrDefaultAsync(tp => tp.Id == id);
            if (travelPackageEntity == null)
            {
                return new ResponseDto<TravelPackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _context.Travels.Remove(travelPackageEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<TravelPackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}