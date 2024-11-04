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

        public HostingsService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // obtener listado de hospedajes 
        public async Task<ResponseDto<List<HostingDto>>> GetHostingsListAsync()
        {
            var hostingEntities = await _context.Hostings
                .Include(h => h.Destination)         
                .Include(h => h.TypeHosting)        
                .ToListAsync();

            var hostingDtos = _mapper.Map<List<HostingDto>>(hostingEntities);

            return new ResponseDto<List<HostingDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = hostingDtos
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
