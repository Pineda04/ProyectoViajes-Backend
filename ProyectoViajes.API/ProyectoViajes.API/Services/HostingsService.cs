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

        public async Task<ResponseDto<List<HostingDto>>> GetHostingsListAsync()
        {
            var hostingEntity = await _context.Hostings.ToListAsync();
            var hostingDtos = _mapper.Map<List<HostingDto>>(hostingEntity);

            return new ResponseDto<List<HostingDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = hostingDtos
            };
        }
        public async Task<ResponseDto<HostingDto>> GetHostingByIdAsync(Guid id)
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

            var hostingDto = _mapper.Map<HostingDto>(hostingEntity);

            return new ResponseDto<HostingDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = hostingDto
            };
        }
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
        public async Task<ResponseDto<HostingDto>> EditAsync(HostingEditDto dto, Guid id)
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

            _mapper.Map<HostingEditDto, HostingEntity>(dto, hostingEntity);

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
