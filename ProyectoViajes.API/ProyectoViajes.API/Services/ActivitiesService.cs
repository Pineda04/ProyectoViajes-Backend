using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        public ActivitiesService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }
        public async Task<ResponseDto<PaginationDto<List<ActivityDto>>>> GetActivitiesListAsync(
            string searchTerm = "", 
            int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var activitiesQuery = _context.Activities.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                activitiesQuery = activitiesQuery
                    .Where(x => (x.Name + " " + x.Description)
                    .ToLower().Contains(searchTerm.ToLower()));
            }

            int totalActivities = await activitiesQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalActivities / PAGE_SIZE);

            var activitiesEntity = await activitiesQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var activitiesDto = _mapper.Map<List<ActivityDto>>(activitiesEntity);

            return new ResponseDto<PaginationDto<List<ActivityDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<ActivityDto>>
                {
                    CurrentPage = page,           
                    PageSize = PAGE_SIZE,         
                    TotalItems = totalActivities, 
                    TotalPages = totalPages,      
                    Items = activitiesDto,        
                    HasPreviousPage = page > 1,   
                    HasNextPage = page < totalPages 
                }
            };
        }
        public async Task<ResponseDto<ActivityDto>> GetActivityByIdAsync(Guid id)
        {
            var activityEntity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);
            if (activityEntity == null)
            {
                return new ResponseDto<ActivityDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            var activityDto = _mapper.Map<ActivityDto>(activityEntity);
            return new ResponseDto<ActivityDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = activityDto
            };
        }
        public async Task<ResponseDto<ActivityDto>> CreateAsync(ActivityCreateDto dto)
        {
            var activityEntity = _mapper.Map<ActivityEntity>(dto);
            _context.Activities.Add(activityEntity);
            await _context.SaveChangesAsync();
            var activityDto = _mapper.Map<ActivityDto>(activityEntity);
            return new ResponseDto<ActivityDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = activityDto
            };
        }
        public async Task<ResponseDto<ActivityDto>> EditAsync(ActivityEditDto dto, Guid id)
        {
            var activityEntity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);
            if (activityEntity == null)
            {
                return new ResponseDto<ActivityDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _mapper.Map(dto, activityEntity);
            _context.Activities.Update(activityEntity);
            await _context.SaveChangesAsync();
            var activityDto = _mapper.Map<ActivityDto>(activityEntity);
            return new ResponseDto<ActivityDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = activityDto
            };
        }
        public async Task<ResponseDto<ActivityDto>> DeleteAsync(Guid id)
        {
            var activityEntity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);
            if (activityEntity == null)
            {
                return new ResponseDto<ActivityDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _context.Activities.Remove(activityEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<ActivityDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}