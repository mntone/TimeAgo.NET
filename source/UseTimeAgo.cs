using System;

namespace TimeAgo
{
	[Flags]
	public enum UseTimeAgo : uint
	{
		UseNow          = 1U << 1,
		UseSecond       = 1U << 2,
		UseMinute       = 1U << 3,
		UseHour         = 1U << 4,
		UseDay          = 1U << 5,
		UseWeek         = 1U << 6,
		UseMonth        = 1U << 7,
		UseYear         = 1U << 8,
		UseDecade       = 1U << 9,
		UseCentury      = 1U << 10,
		UseComplex      = 1U << 31,
	}

	internal static class UseTimeAgoExtensions
	{
		public static bool HasAnyFlag(this UseTimeAgo self, UseTimeAgo flag)
			=> (self & flag) != 0;
	}
}
