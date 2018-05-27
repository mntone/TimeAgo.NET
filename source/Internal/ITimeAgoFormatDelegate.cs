using System;
using TimeAgo.Globalization;

namespace TimeAgo.Internal
{
	internal interface ITimeAgoFormatDelegate
	{
		string NowPattern { get; }

		string SecondsPattern { get; }
		string SecondPattern { get; }

		string MinutesSecondsPattern { get; }
		string MinuteSecondsPattern { get; }
		string MinutesSecondPattern { get; }
		string MinuteSecondPattern { get; }

		string MinutesPattern { get; }
		string MinutePattern { get; }

		string HoursMinutesPattern { get; }
		string HourMinutesPattern { get; }
		string HoursMinutePattern { get; }
		string HourMinutePattern { get; }

		string HoursPattern { get; }
		string HourPattern { get; }

		string DaysHoursPattern { get; }
		string DayHoursPattern { get; }
		string DaysHourPattern { get; }
		string DayHourPattern { get; }

		string DaysPattern { get; }
		string DayPattern { get; }

		string WeeksPattern { get; }
		string WeekPattern { get; }

		string MonthsPattern { get; }
		string MonthPattern { get; }

		string YearsPattern { get; }
		string YearPattern { get; }

		string DecadesPattern { get; }
		string DecadePattern { get; }

		string CenturysPattern { get; }
		string CenturyPattern { get; }
	}

	internal sealed class ShortTimeAgoFormatDelegate : ITimeAgoFormatDelegate
	{
		internal TimeAgoFormatInfo Info { get; }

		public ShortTimeAgoFormatDelegate(TimeAgoFormatInfo info) => Info = info;

		public string NowPattern => throw new InvalidOperationException();

		public string SecondsPattern => Info.ShortSecondsPattern;
		public string SecondPattern => throw new InvalidOperationException();

		public string MinutesSecondsPattern => Info.ShortMinutesSecondsPattern;
		public string MinuteSecondsPattern => throw new InvalidOperationException();
		public string MinutesSecondPattern => throw new InvalidOperationException();
		public string MinuteSecondPattern => throw new InvalidOperationException();

		public string MinutesPattern => Info.ShortMinutesPattern;
		public string MinutePattern => throw new InvalidOperationException();

		public string HoursMinutesPattern => Info.ShortHoursMinutesPattern;
		public string HourMinutesPattern => throw new InvalidOperationException();
		public string HoursMinutePattern => throw new InvalidOperationException();
		public string HourMinutePattern => throw new InvalidOperationException();

		public string HoursPattern => Info.ShortHoursPattern;
		public string HourPattern => throw new InvalidOperationException();

		public string DaysHoursPattern => Info.ShortDaysHoursPattern;
		public string DayHoursPattern => throw new InvalidOperationException();
		public string DaysHourPattern => throw new InvalidOperationException();
		public string DayHourPattern => throw new InvalidOperationException();

		public string DaysPattern => Info.ShortDaysPattern;
		public string DayPattern => throw new InvalidOperationException();

		public string WeeksPattern => Info.ShortWeeksPattern;
		public string WeekPattern => throw new InvalidOperationException();

		public string MonthsPattern => Info.ShortMonthsPattern;
		public string MonthPattern => throw new InvalidOperationException();

		public string YearsPattern => Info.ShortYearsPattern;
		public string YearPattern => throw new InvalidOperationException();

		public string DecadesPattern => Info.ShortDecadesPattern;
		public string DecadePattern => throw new InvalidOperationException();

		public string CenturysPattern => Info.ShortCenturysPattern;
		public string CenturyPattern => throw new InvalidOperationException();
	}

	internal sealed class MediumTimeAgoFormatDelegate : ITimeAgoFormatDelegate
	{
		internal TimeAgoFormatInfo Info { get; }

		public MediumTimeAgoFormatDelegate(TimeAgoFormatInfo info) => Info = info;

		public string NowPattern => Info.MediumNowPattern;

		public string SecondsPattern => Info.MediumSecondsPattern;
		public string SecondPattern => Info.MediumSecondPattern;

		public string MinutesSecondsPattern => Info.MediumMinutesSecondsPattern;
		public string MinuteSecondsPattern => Info.MediumMinuteSecondsPattern;
		public string MinutesSecondPattern => Info.MediumMinutesSecondPattern;
		public string MinuteSecondPattern => Info.MediumMinuteSecondPattern;

		public string MinutesPattern => Info.MediumMinutesPattern;
		public string MinutePattern => Info.MediumMinutePattern;

		public string HoursMinutesPattern => Info.MediumHoursMinutesPattern;
		public string HourMinutesPattern => Info.MediumHourMinutesPattern;
		public string HoursMinutePattern => Info.MediumHoursMinutePattern;
		public string HourMinutePattern => Info.MediumHourMinutePattern;

		public string HoursPattern => Info.MediumHoursPattern;
		public string HourPattern => Info.MediumHourPattern;

		public string DaysHoursPattern => Info.MediumDaysHoursPattern;
		public string DayHoursPattern => Info.MediumDayHoursPattern;
		public string DaysHourPattern => Info.MediumDaysHourPattern;
		public string DayHourPattern => Info.MediumDayHourPattern;

		public string DaysPattern => Info.MediumDaysPattern;
		public string DayPattern => Info.MediumDayPattern;

		public string WeeksPattern => Info.MediumWeeksPattern;
		public string WeekPattern => Info.MediumWeekPattern;

		public string MonthsPattern => Info.MediumMonthsPattern;
		public string MonthPattern => Info.MediumMonthPattern;

		public string YearsPattern => Info.MediumYearsPattern;
		public string YearPattern => Info.MediumYearPattern;

		public string DecadesPattern => Info.MediumDecadesPattern;
		public string DecadePattern => Info.MediumDecadePattern;

		public string CenturysPattern => Info.MediumCenturysPattern;
		public string CenturyPattern => Info.MediumCenturyPattern;
	}

	internal sealed class LongTimeAgoFormatDelegate : ITimeAgoFormatDelegate
	{
		internal TimeAgoFormatInfo Info { get; }

		public LongTimeAgoFormatDelegate(TimeAgoFormatInfo info) => Info = info;

		public string NowPattern => Info.LongNowPattern;

		public string SecondsPattern => Info.LongSecondsPattern;
		public string SecondPattern => Info.LongSecondPattern;

		public string MinutesSecondsPattern => Info.LongMinutesSecondsPattern;
		public string MinuteSecondsPattern => Info.LongMinuteSecondsPattern;
		public string MinutesSecondPattern => Info.LongMinutesSecondPattern;
		public string MinuteSecondPattern => Info.LongMinuteSecondPattern;

		public string MinutesPattern => Info.LongMinutesPattern;
		public string MinutePattern => Info.LongMinutePattern;

		public string HoursMinutesPattern => Info.LongHoursMinutesPattern;
		public string HourMinutesPattern => Info.LongHourMinutesPattern;
		public string HoursMinutePattern => Info.LongHoursMinutePattern;
		public string HourMinutePattern => Info.LongHourMinutePattern;

		public string HoursPattern => Info.LongHoursPattern;
		public string HourPattern => Info.LongHourPattern;

		public string DaysHoursPattern => Info.LongDaysHoursPattern;
		public string DayHoursPattern => Info.LongDayHoursPattern;
		public string DaysHourPattern => Info.LongDaysHourPattern;
		public string DayHourPattern => Info.LongDayHourPattern;

		public string DaysPattern => Info.LongDaysPattern;
		public string DayPattern => Info.LongDayPattern;

		public string WeeksPattern => Info.LongWeeksPattern;
		public string WeekPattern => Info.LongWeekPattern;

		public string MonthsPattern => Info.LongMonthsPattern;
		public string MonthPattern => Info.LongMonthPattern;

		public string YearsPattern => Info.LongYearsPattern;
		public string YearPattern => Info.LongYearPattern;

		public string DecadesPattern => Info.LongDecadesPattern;
		public string DecadePattern => Info.LongDecadePattern;

		public string CenturysPattern => Info.LongCenturysPattern;
		public string CenturyPattern => Info.LongCenturyPattern;
	}
}
