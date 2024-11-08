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
        public TravelPackagesService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseDto<List<TravelPackageDto>>> GetTravelPackagesListAsync()
        {
            var travelPackagesEntity = await _context.Travels
                .Include(tp => tp.Activities).Include(tp => tp.Assessments).ToListAsync();
            var travelPackagesDtos = _mapper.Map<List<TravelPackageDto>>(travelPackagesEntity);
            return new ResponseDto<List<TravelPackageDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = travelPackagesDtos
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