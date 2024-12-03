using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Dashboard;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }
        [HttpGet("info")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<DashboardDto>>> GetDashboardInfo() 
        {
            var dashboardResponse = await _dashboardService.GetDashboardAsync();
            return StatusCode(dashboardResponse.StatusCode, dashboardResponse);
        }
    }
}