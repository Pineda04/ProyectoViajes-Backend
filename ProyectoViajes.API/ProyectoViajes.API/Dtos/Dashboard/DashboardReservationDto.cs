using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoViajes.API.Dtos.Dashboard
{
    public class DashboardReservationDto
    {
        public Guid Id { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}