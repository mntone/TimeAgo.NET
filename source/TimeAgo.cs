using System;
using TimeAgo.Globalization;
using TimeAgo.Internal;

namespace TimeAgo
{
	public static class TimeAgo
	{
		private const string VALUE_IS_FUTURE = "{0} is future";

		private const long DaysPerWeek = 7;
		private const long DaysPerMonth = 30;
		private const long DaysPerYear = 365;
		private const long DaysPerDecade = 4 * 365 + 2 * 366;
		private const long DaysPerCentury = 75 * 365 + 25 * 366;

		private const long TicksPerWeek = DaysPerWeek * TimeSpan.TicksPerDay;
		private const long TicksPerMonth = DaysPerMonth * TimeSpan.TicksPerDay;
		private const long TicksPerYear = DaysPerYear * TimeSpan.TicksPerDay;
		private const long TicksPerDecade = DaysPerDecade * TimeSpan.TicksPerDay;
		private const long TicksPerCentury = DaysPerCentury * TimeSpan.TicksPerDay;

		#region Short

		public static string ToShortTimeAgo(this DateTime value)
			=> value.ToShortTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToShortTimeAgo(this DateTime value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (value.Kind == DateTimeKind.Unspecified) throw new ArgumentException(nameof(value));

			var current = DateTime.UtcNow;
			var utcValue = value.ToUniversalTime();
			if (utcValue > current)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = current - utcValue;
			return offset.ToShortTimeAgo(provider);
		}

		public static string ToShortTimeAgo(this DateTimeOffset value)
			=> value.ToShortTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToShortTimeAgo(this DateTimeOffset value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			var current = DateTimeOffset.UtcNow;
			var utcValue = value.ToUniversalTime();
			if (utcValue > current)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = current - utcValue;
			return offset.ToShortTimeAgo(provider);
		}

		public static string ToShortTimeAgo(this TimeSpan value)
			=> value.ToShortTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToShortTimeAgo(this TimeSpan value, IFormatProvider provider)
		{
			var formatter = (TimeAgoFormatInfo)provider.GetFormat(typeof(TimeAgoFormatInfo));
			if (formatter == null) throw new ArgumentException(nameof(provider));
			if (!formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour))
				throw new ArgumentException(nameof(provider));

			var formatterDelegate = formatter.Short;
			var ticks = value.Ticks;
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseSecond))
			{
				// Xs
				if (ticks < TimeSpan.TicksPerMinute)
				{
					return string.Format(formatterDelegate.SecondsPattern, value.Seconds);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMinute))
			{
				if (value.Seconds != 0
					&& formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseSecond | UseTimeAgo.UseComplex))
				{
					// Xm Ys
					if (ticks < TimeSpan.TicksPerHour)
					{
						return string.Format(formatterDelegate.MinutesSecondsPattern, value.Minutes, value.Seconds);
					}
				}
				else
				{
					// Xm
					if (ticks < TimeSpan.TicksPerHour)
					{
						return string.Format(formatterDelegate.MinutesPattern, value.Minutes);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour))
			{
				if (value.Minutes != 0
					&& formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMinute | UseTimeAgo.UseComplex))
				{
					// Xh Ym
					if (ticks < TimeSpan.TicksPerDay)
					{
						return string.Format(formatterDelegate.HoursMinutesPattern, value.Hours, value.Minutes);
					}
					if (!formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDay | UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.HoursMinutesPattern, (int)value.TotalHours, value.Minutes);
					}
				}
				else
				{
					// Xh
					if (ticks < TimeSpan.TicksPerDay)
					{
						return string.Format(formatterDelegate.HoursPattern, value.Hours);
					}
					if (!formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDay | UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.HoursPattern, (int)value.TotalHours);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseDay))
			{
				if (value.Hours != 0
					&& formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour | UseTimeAgo.UseComplex))
				{
					// Xd Yh
					if (ticks < TimeSpan.TicksPerMinute
						|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.DaysHoursPattern, value.Days, value.Hours);
					}
				}
				else
				{
					// Xd
					if (ticks < TicksPerWeek
						|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.DaysPattern, value.Days);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseWeek))
			{
				// Xw
				if (ticks < TicksPerMonth
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.WeeksPattern, value.Days / DaysPerWeek);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMonth))
			{
				// Xmon
				if (ticks < TicksPerYear
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.MonthsPattern, value.Days / DaysPerMonth);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseYear))
			{
				// Xy
				if (ticks < TicksPerDecade
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.YearsPattern, value.Days / DaysPerYear);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseDecade))
			{
				// X0y
				if (ticks < TicksPerCentury
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.DecadesPattern, value.Days / DaysPerDecade);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseCentury))
			{
				// Xc
				return string.Format(formatterDelegate.CenturysPattern, value.Days / DaysPerCentury);
			}
			throw new InvalidOperationException();
		}

		#endregion

		#region Medium/Long

		public static string ToMediumTimeAgo(this DateTime value)
			=> value.ToMediumTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToMediumTimeAgo(this DateTime value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (value.Kind == DateTimeKind.Unspecified) throw new ArgumentException(nameof(value));

			var currentValue = DateTime.UtcNow;
			var pastValue = value.ToUniversalTime();
			if (pastValue > currentValue)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = currentValue - pastValue;
			return pastValue.ToMediumTimeAgo(provider);
		}

		public static string ToMediumTimeAgo(this DateTimeOffset value)
			=> value.ToMediumTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToMediumTimeAgo(this DateTimeOffset value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			var currentValue = DateTimeOffset.UtcNow;
			var pastValue = value.ToUniversalTime();
			if (pastValue > currentValue)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = currentValue - pastValue;
			return pastValue.ToMediumTimeAgo(provider);
		}

		public static string ToLongTimeAgo(this DateTime value)
			=> value.ToLongTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToLongTimeAgo(this DateTime value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (value.Kind == DateTimeKind.Unspecified) throw new ArgumentException(nameof(value));

			var currentValue = DateTime.UtcNow;
			var pastValue = value.ToUniversalTime();
			if (pastValue > currentValue)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = currentValue - pastValue;
			return pastValue.ToLongTimeAgo(provider);
		}

		public static string ToLongTimeAgo(this DateTimeOffset value)
			=> value.ToLongTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToLongTimeAgo(this DateTimeOffset value, IFormatProvider provider)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			var currentValue = DateTimeOffset.UtcNow;
			var pastValue = value.ToUniversalTime();
			if (pastValue > currentValue)
				throw new ArgumentOutOfRangeException(nameof(value), string.Format(VALUE_IS_FUTURE, nameof(value)));

			var offset = currentValue - pastValue;
			return pastValue.ToLongTimeAgo(provider);
		}

		public static string ToMediumTimeAgo(this TimeSpan value)
			=> value.ToMediumTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToMediumTimeAgo(this TimeSpan value, IFormatProvider provider)
		{
			var formatter = (TimeAgoFormatInfo)provider.GetFormat(typeof(TimeAgoFormatInfo));
			if (formatter == null) throw new ArgumentException(nameof(provider));
			if (!formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour))
				throw new ArgumentException(nameof(provider));

			var formatterDelegate = formatter.Medium;
			return value.ToTimeAgo(provider, formatter, formatterDelegate);
		}

		public static string ToLongTimeAgo(this TimeSpan value)
			=> value.ToLongTimeAgo(TimeAgoFormatInfo.CultureInfo);

		public static string ToLongTimeAgo(this TimeSpan value, IFormatProvider provider)
		{
			var formatter = (TimeAgoFormatInfo)provider.GetFormat(typeof(TimeAgoFormatInfo));
			if (formatter == null) throw new ArgumentException(nameof(provider));
			if (!formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour))
				throw new ArgumentException(nameof(provider));

			var formatterDelegate = formatter.Long;
			return value.ToTimeAgo(provider, formatter, formatterDelegate);
		}

		internal static string ToTimeAgo(this TimeSpan value, IFormatProvider provider, TimeAgoFormatInfo formatter, ITimeAgoFormatDelegate formatterDelegate)
		{
			var ticks = value.Ticks;
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseNow))
			{
				// Now
				if (ticks <= formatter.SwitchUntilNowInSeconds * TimeSpan.TicksPerSecond)
				{
					return formatterDelegate.NowPattern;
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseSecond))
			{
				// 1s
				if (ticks < 2 * TimeSpan.TicksPerSecond)
				{
					return formatterDelegate.SecondPattern;
				}
				// Xs
				if (ticks < TimeSpan.TicksPerMinute)
				{
					return string.Format(formatterDelegate.SecondsPattern, value.Seconds);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMinute))
			{
				// 1m
				if (ticks < TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond)
				{
					return formatterDelegate.MinutePattern;
				}
				if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseSecond | UseTimeAgo.UseComplex))
				{
					// 1m Xs
					if (ticks < 2 * TimeSpan.TicksPerMinute)
					{
						return string.Format(formatterDelegate.MinuteSecondsPattern, value.Seconds);
					}
					// Xm / Xm 1s / Xm Ys
					if (ticks < TimeSpan.TicksPerHour)
					{
						if (value.Seconds == 0)
							return string.Format(formatterDelegate.MinutesPattern, value.Minutes);
						if (value.Seconds == 1)
							return string.Format(formatterDelegate.MinutesSecondPattern, value.Minutes);

						return string.Format(formatterDelegate.MinutesSecondsPattern, value.Minutes, value.Seconds);
					}
				}
				else
				{
					// Xm
					if (ticks < TimeSpan.TicksPerHour)
					{
						return string.Format(formatterDelegate.MinutesPattern, value.Minutes);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour))
			{
				// 1h
				if (ticks < TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute)
				{
					return formatterDelegate.HourPattern;
				}
				if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMinute | UseTimeAgo.UseComplex))
				{
					// 1h Xm
					if (ticks < 2 * TimeSpan.TicksPerHour)
					{
						return string.Format(formatterDelegate.HourMinutesPattern, value.Minutes);
					}
					// Xh / Xh 1m / Xh Ym
					if (ticks < TimeSpan.TicksPerDay)
					{
						if (value.Minutes == 0)
							return string.Format(formatterDelegate.HoursPattern, value.Hours);
						if (value.Minutes == 1)
							return string.Format(formatterDelegate.HoursMinutePattern, value.Hours);

						return string.Format(formatterDelegate.HoursMinutesPattern, value.Hours, value.Minutes);
					}
					if (!formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDay | UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						if (value.Minutes == 0)
							return string.Format(formatterDelegate.HoursPattern, (int)value.TotalHours);
						if (value.Minutes == 1)
							return string.Format(formatterDelegate.HoursMinutePattern, (int)value.TotalHours);

						return string.Format(formatterDelegate.HoursMinutesPattern, (int)value.TotalHours, value.Minutes);
					}
				}
				else
				{
					// Xh
					if (ticks < TimeSpan.TicksPerDay)
					{
						return string.Format(formatterDelegate.HoursPattern, value.Hours);
					}
					if (!formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDay | UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.HoursPattern, (int)value.TotalHours);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseDay))
			{
				// 1d
				if (ticks < TimeSpan.TicksPerDay + TimeSpan.TicksPerHour)
				{
					return formatterDelegate.DayPattern;
				}
				if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseHour | UseTimeAgo.UseComplex))
				{
					// 1d Xh
					if (ticks < 2 * TimeSpan.TicksPerDay)
					{
						return string.Format(formatterDelegate.DayHoursPattern, value.Hours);
					}
					// Xd / Xd 1h / Xd Yh
					if (ticks < TimeSpan.TicksPerMinute
						|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						if (value.Hours == 0)
							return string.Format(formatterDelegate.DaysPattern, value.Days);
						if (value.Hours == 1)
							return string.Format(formatterDelegate.DaysHourPattern, value.Days);

						return string.Format(formatterDelegate.DaysHoursPattern, value.Days, value.Hours);
					}
				}
				else
				{
					// Xd
					if (ticks < TicksPerWeek
						|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseWeek | UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
					{
						return string.Format(formatterDelegate.DaysPattern, value.Days);
					}
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseWeek))
			{
				// 1w
				if (ticks < 2 * TicksPerWeek)
				{
					return formatterDelegate.WeekPattern;
				}
				// Xw
				if (ticks < TicksPerMonth
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseMonth | UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.WeeksPattern, value.Days / DaysPerWeek);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseMonth))
			{
				// 1 month
				if (ticks < 2 * TicksPerMonth)
				{
					return formatterDelegate.MonthPattern;
				}
				// X months
				if (ticks < TicksPerYear
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseYear | UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.MonthsPattern, value.Days / DaysPerMonth);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseYear))
			{
				// 1y
				if (ticks < 2 * TicksPerYear)
				{
					return formatterDelegate.YearPattern;
				}
				// Xy
				if (ticks < TicksPerDecade
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseDecade | UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.YearsPattern, value.Days / DaysPerYear);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseDecade))
			{
				// 1 decade
				if (ticks < 2 * TicksPerDecade)
				{
					return formatterDelegate.DecadePattern;
				}
				// X decades
				if (ticks < TicksPerCentury
					|| !formatter.UseTimeAgo.HasAnyFlag(UseTimeAgo.UseCentury))
				{
					return string.Format(formatterDelegate.DecadesPattern, value.Days / DaysPerDecade);
				}
			}
			if (formatter.UseTimeAgo.HasFlag(UseTimeAgo.UseCentury))
			{
				// 1 centry
				if (ticks < 2 * TicksPerCentury)
				{
					return formatterDelegate.CenturyPattern;
				}
				// X centurys
				return string.Format(formatterDelegate.CenturysPattern, value.Days / DaysPerCentury);
			}
			throw new InvalidOperationException();
		}

		#endregion
	}
}
