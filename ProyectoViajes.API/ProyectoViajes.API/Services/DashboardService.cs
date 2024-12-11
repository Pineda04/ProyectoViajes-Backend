using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Dashboard;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        public DashboardService(
            ProyectoViajesContext context,
            IMapper mapper
            )
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<ResponseDto<DashboardDto>> GetDashboardAsync()
        {
            var users = await _context.Users.
                OrderByDescending(p => p.FirstName).Take(5).ToListAsync();
            var activities = await _context.Activities.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var assessments = await _context.Assessments.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var destinations = await _context.Destinations.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var flights = await _context.Flights.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var hostings = await _context.Hostings.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var pointInterests = await _context.PointsInterest.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var reservations = await _context.Reservations.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var travelPackages = await _context.Travels.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var typeFlights = await _context.TypesFlight.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            var typeHostings = await _context.TypesHosting.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();
            
            var dashboardDto = new DashboardDto
            {
                UsersCount = await _context.Users.CountAsync(),
                ActivitiesCount = await _context.Activities.CountAsync(),
                AssessmentsCount = await _context.Assessments.CountAsync(),
                DestinationsCount = await _context.Destinations.CountAsync(),
                FlightsCount = await _context.Flights.CountAsync(),
                HostingsCount = await _context.Hostings.CountAsync(),
                PointsInterestCount = await _context.PointsInterest.CountAsync(),
                ReservationsCount = await _context.Reservations.CountAsync(),
                TravelPackagesCount = await _context.Travels.CountAsync(),
                TypesFlightsCount = await _context.TypesFlight.CountAsync(),
                TypesHostingsCount = await _context.TypesHosting.CountAsync(),

                Users = _mapper.Map<List<DashboardUserDto>>(users),
                Activities = _mapper.Map<List<DashboardActivityDto>>(activities),
                Assessments = _mapper.Map<List<DashboardAssessmentDto>>(assessments),
                Destinations = _mapper.Map<List<DashboardDestinationDto>>(destinations),
                Flights = _mapper.Map<List<DashboardFlightDto>>(flights),
                Hostings = _mapper.Map<List<DashboardHostingDto>>(hostings),
                PointsInterest = _mapper.Map<List<DashboardPointInterestDto>>(pointInterests),
                Reservations = _mapper.Map<List<DashboardReservationDto>>(reservations),
                TravelPackages = _mapper.Map<List<DashboardTravelPackageDto>>(travelPackages),
                TypesFlights = _mapper.Map<List<DashboardTypeFlightDto>>(typeFlights),
                TypesHostings = _mapper.Map<List<DashboardTypeHostingDto>>(typeHostings),
            };
            return new ResponseDto<DashboardDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Datos obtenidos correctamente",
                Data = dashboardDto
            };
        }
    }
}