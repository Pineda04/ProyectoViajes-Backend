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

        public TypesFlightService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<TypeFlightDto>>> GetTypesFlightListAsync()
        {
            var typeFlightEntity = await _context.TypesFlight.ToListAsync();
            var typeFlightDtos = _mapper.Map<List<TypeFlightDto>>(typeFlightEntity);

            return new ResponseDto<List<TypeFlightDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = typeFlightDtos
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
