using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuditService(
            IHttpContextAccessor httpContextAccessor    
            )
        {
            _contextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var idClaim = _contextAccessor.HttpContext
                .User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
                
            return idClaim.Value;
        }
    }
}