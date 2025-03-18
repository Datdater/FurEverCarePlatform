using System.Security.Claims;
using FurEverCarePlatform.Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class ClaimService(IHttpContextAccessor httpContextAccessor) : IClaimService
    {
        public Guid GetCurrentUser
        {
            get
            {
                var userId = httpContextAccessor
                    .HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
                    ?.Value;
                return Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;
            }
        }
    }
}
