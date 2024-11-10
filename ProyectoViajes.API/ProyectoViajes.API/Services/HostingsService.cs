using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Services.Interfaces;


namespace ProyectoViajes.API.Services
{
    public class HostingsService : IHostingsService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public HostingsService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        // obtener listado de hospedajes 
        public async Task<ResponseDto<PaginationDto<List<HostingDto>>>> GetHostingsListAsync(
            string searchTerm = "", 
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var hostingsQuery = _context.Hostings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                hostingsQuery = hostingsQuery.Where(h => h.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                          h.Destination.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                          h.TypeHosting.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = await hostingsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);

            var hostingEntities = await hostingsQuery
                .Include(h => h.Destination)  
                .Include(h => h.TypeHosting)  
                .OrderBy(h => h.CreatedDate)  
                .Skip(startIndex)  
                .Take(PAGE_SIZE)
                .ToListAsync();

            var hostingDtos = _mapper.Map<List<HostingDto>>(hostingEntities);

            return new ResponseDto<PaginationDto<List<HostingDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<HostingDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = hostingDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }

        // obtener por id
        public async Task<ResponseDto<HostingDto>> GetHostingByIdAsync(Guid id)
        {
            var hostingEntity = await _context.Hostings
                .Include(h => h.Destination)
                .Include(h => h.TypeHosting)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hostingEntity == null)
            {
                return new ResponseDto<HostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            var hostingDto = _mapper.Map<HostingDto>(hostingEntity);

            return new ResponseDto<HostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = hostingDto
            };
        }

        // Crear un nuevo hospedaje  
        public async Task<ResponseDto<HostingDto>> CreateAsync(HostingCreateDto dto)
        {
            var hostingEntity = _mapper.Map<HostingEntity>(dto);
            _context.Hostings.Add(hostingEntity);
            await _context.SaveChangesAsync();
            var hostingDto = _mapper.Map<HostingDto>(hostingEntity);
            return new ResponseDto<HostingDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = hostingDto
            };
        }

        // Editar hospedaje existente
        public async Task<ResponseDto<HostingDto>> EditAsync(HostingEditDto dto, Guid id)
        {
            var hostingEntity = await _context.Hostings
                .Include(h => h.Destination)
                .Include(h => h.TypeHosting)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hostingEntity == null)
            {
                return new ResponseDto<HostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            // Validar relaciones en caso de que el destino o tipo de hospedaje cambien
            var destinationExists = await _context.Destinations.AnyAsync(d => d.Id == dto.DestinationId);
            var typeHostingExists = await _context.TypesHosting.AnyAsync(th => th.Id == dto.TypeHostingId);

            if (!destinationExists || !typeHostingExists)
            {
                return new ResponseDto<HostingDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "no se encontro relacion"
                };
            }

            _mapper.Map(dto, hostingEntity);
            _context.Hostings.Update(hostingEntity);
            await _context.SaveChangesAsync();

            var hostingDto = _mapper.Map<HostingDto>(hostingEntity);

            return new ResponseDto<HostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = hostingDto
            };
        }

        // Eliminar hospedaje
        public async Task<ResponseDto<HostingDto>> DeleteAsync(Guid id)
        {
            var hostingEntity = await _context.Hostings.FirstOrDefaultAsync(h => h.Id == id);

            if (hostingEntity == null)
            {
                return new ResponseDto<HostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.Hostings.Remove(hostingEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<HostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}
