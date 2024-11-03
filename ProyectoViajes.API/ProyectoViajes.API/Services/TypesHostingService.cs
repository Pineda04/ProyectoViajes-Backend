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

        public TypesHostingService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<TypeHostingDto>>> GetTypesHostingListAsync()
        {
            var typeHostingEntity = await _context.TypesHosting.ToListAsync();
            var typeHostingDtos = _mapper.Map<List<TypeHostingDto>>(typeHostingEntity);

            return new ResponseDto<List<TypeHostingDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = typeHostingDtos
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
            var typeHostingEntity = await _context.Hostings.FirstOrDefaultAsync(t => t.Id == id);

            if (typeHostingEntity == null)
            {
                return new ResponseDto<TypeHostingDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }

            _context.Hostings.Remove(typeHostingEntity);
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
