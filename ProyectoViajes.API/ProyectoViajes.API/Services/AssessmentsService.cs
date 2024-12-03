using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public AssessmentsService(ProyectoViajesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<AssessmentDto>>> GetAllAssessmentsAsync()
        {
            var assessmentEntities = await _context.Assessments
                .Include(a => a.TravelPackage)
                .ToListAsync();
            
            var assessmentDtos = _mapper.Map<List<AssessmentDto>>(assessmentEntities);
            
            return new ResponseDto<List<AssessmentDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Valoraciones obtenida correctamente.",
                Data = assessmentDtos
            };
        }

        public async Task<ResponseDto<AssessmentDto>> GetAssessmentByIdAsync(Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .Include(a => a.TravelPackage)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assessmentEntity is null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Valoración."
                };
            }

            var assessmentDto = _mapper.Map<AssessmentDto>(assessmentEntity);
            
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Valoración encontrada.",
                Data = assessmentDto
            };
        }

        public async Task<ResponseDto<List<AssessmentDto>>> GetAssessmentsByTravelPackageAsync(Guid travelPackageId)
        {
            var assessmentEntities = await _context.Assessments
                .Include(a => a.TravelPackage)
                .Where(a => a.TravelPackageId == travelPackageId)
                .ToListAsync();

            var assessmentDtos = _mapper.Map<List<AssessmentDto>>(assessmentEntities);

            return new ResponseDto<List<AssessmentDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Valoraciones del Paquete de Viaje obtenida correctamente.",
                Data = assessmentDtos
            };
        }

        public async Task<ResponseDto<AssessmentDto>> CreateAsync(AssessmentCreateDto dto)
        {
            // Verificar que el paquete de viaje exista
            var travelPackageExists = await _context.TravelPackages
                .AnyAsync(tp => tp.Id == dto.TravelPackageId);

            if (!travelPackageExists)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "El Paquete de Viaje no existe."
                };
            }

            var assessmentEntity = _mapper.Map<AssessmentEntity>(dto);
            
            _context.Assessments.Add(assessmentEntity);
            await _context.SaveChangesAsync();

            var assessmentDto = _mapper.Map<AssessmentDto>(assessmentEntity);
            
            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Valoración creada correctamente.",
                Data = assessmentDto
            };
        }

        public async Task<ResponseDto<AssessmentDto>> EditAsync(AssessmentEditDto dto, Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assessmentEntity is null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Valoración."
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
                Message = "Valoración editada correctamente.",
                Data = assessmentDto
            };
        }

        public async Task<ResponseDto<AssessmentDto>> DeleteAsync(Guid id)
        {
            var assessmentEntity = await _context.Assessments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assessmentEntity is null)
            {
                return new ResponseDto<AssessmentDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontró la Valoración."
                };
            }

            _context.Assessments.Remove(assessmentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<AssessmentDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Valoración eliminada correctamente."
            };
        }
    }
}