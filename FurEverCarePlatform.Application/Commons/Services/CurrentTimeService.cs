
using FurEverCarePlatform.Application.Commons.Interfaces;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class CurrentTimeService : ICurrentTime
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.UtcNow;
		}
	}
}
