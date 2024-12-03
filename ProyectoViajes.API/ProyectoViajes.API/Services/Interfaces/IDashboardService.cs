using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Dashboard;

namespace ProyectoViajes.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseDto<DashboardDto>> GetDashboardAsync();
    }
}