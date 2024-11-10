using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Common;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IAssessmentsService
    {
        Task<ResponseDto<PaginationDto<List<AssessmentDto>>>> GetAssessmentsListAsync(
           string searchTerm = "", int page = 1
        );
        Task<ResponseDto<AssessmentDto>> GetAssessmentByIdAsync(Guid id);
        Task<ResponseDto<AssessmentDto>> CreateAsync(AssessmentCreateDto dto);
        Task<ResponseDto<AssessmentDto>> EditAsync(AssessmentEditDto dto, Guid id);
        Task<ResponseDto<AssessmentDto>> DeleteAsync(Guid id);
    }
}