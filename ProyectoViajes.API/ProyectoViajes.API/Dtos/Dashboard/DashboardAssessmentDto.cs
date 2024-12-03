using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoViajes.API.Dtos.Dashboard
{
    public class DashboardAssessmentDto
    {
        public Guid Id { get; set; }
        public int Stars { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}