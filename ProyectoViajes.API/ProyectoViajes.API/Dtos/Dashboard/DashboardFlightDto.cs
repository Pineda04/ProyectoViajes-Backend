using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoViajes.API.Dtos.Dashboard
{
    public class DashboardFlightDto
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}