//using FurEverCarePlatform.Application.Commons.Interfaces;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;

//namespace FurEverCarePlatform.Application.Commons.Services
//{
//	public class ClaimService : IClaimService
//	{
//		private readonly IHttpContextAccessor _httpContextAccessor;

//		public ClaimService(IHttpContextAccessor httpContextAccessor)
//		{
//			_httpContextAccessor = httpContextAccessor;
//		}

//		public Guid GetCurrentUser
//		{
//			get
//			{
//				var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//				return Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;
//			}
//		}
//	}
//}
