using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class AssessmentsService : IAssessmentsService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public AssessmentsService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<AssessmentDto>>>> GetAssessmentsListAsync(
            string searchTerm = "",
            int page = 1
        )
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var assessmentsQuery = _context.Assessments
                .Include(a => a.User)
                .Include(tp => tp.TravelPackage)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Buscar por comentario o cantidad de estrellas
                assessmentsQuery = assessmentsQuery
                    .Where(x => (x.Comment != null && x.Comment.ToLower().Contains(searchTerm.ToLower()))
                    || x.Stars.ToString().Contains(searchTerm));
            }

            int totalAssessments = await assessmentsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalAssessments / PAGE_SIZE);

            var assessmentsEntity = await assessmentsQuery
                .OrderByDescending(x => x.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var assessmentsDto = _mapper.Map<List<AssessmentDto>>(assessmentsEntity);

            return new ResponseDto<PaginationDto<List<AssessmentDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<AssessmentDto>>
                {
                    CurrentPage = page,           
                    PageSize = PAGE_SIZE,         
                    TotalItems = totalAssessments, 
                    TotalPages = totalPages,      
                    Items = assessmentsDto,        
                    HasPreviousPage = page > 1,   
                    HasNextPage = page < totalPages
                }
            };
        }

        public async Task<ResponseDto<AssessmentDto>> GetAssessmentByIdAsync(Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .Include(u => u.User)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (assessmentEntity == null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            var assessmentDto = _mapper.Map<AssessmentDto>(assessmentEntity);
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = assessmentDto
            };
        }
        public async Task<ResponseDto<AssessmentDto>> CreateAsync(AssessmentCreateDto dto)
        {
            var assessmentEntity = _mapper.Map<AssessmentEntity>(dto);
            _context.Assessments.Add(assessmentEntity);
            await _context.SaveChangesAsync();
            var assessentDto = _mapper.Map<AssessmentDto>(assessmentEntity);
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstants.CREATE_SUCCESS,
                Data = assessentDto
            };
        }
        public async Task<ResponseDto<AssessmentDto>> EditAsync(AssessmentEditDto dto, Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .FirstOrDefaultAsync(a => a.Id == id);
            if (assessmentEntity == null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _mapper.Map(dto, assessmentEntity);
            _context.Assessments.Update(assessmentEntity);
            await _context.SaveChangesAsync();
            var assessmentDto = _mapper.Map<AssessmentDto>(assessmentEntity);
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = assessmentDto
            };
        }
        public async Task<ResponseDto<AssessmentDto>> DeleteAsync(Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .FirstOrDefaultAsync(a => a.Id == id);
            if (assessmentEntity == null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _context.Assessments.Remove(assessmentEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}