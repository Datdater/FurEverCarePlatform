
using FurEverCarePlatform.Application.Commons.Interfaces;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class CurrentTimeService : ICurrentTime
	{
		public DateTime GetCurrentTime()
		{
			return TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
		}
	}
}
