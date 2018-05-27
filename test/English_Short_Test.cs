//#define HEAVY_TEST

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using TimeAgo.Globalization;

namespace TimeAgo.Test
{
	[TestFixture]
	public class English_Short_Test
	{
		private TimeAgoFormatInfo _infoForWeek, _infoForMonth, _infoForYear, _infoForDecade, _infoForCentury;

		[OneTimeSetUp]
		public void Init()
		{
			var lang = "en-US";
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
			Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

			_infoForWeek = (TimeAgoFormatInfo)TimeAgoFormatInfo.CultureInfo.Clone();
			_infoForWeek.UseTimeAgo |= UseTimeAgo.UseWeek;

			_infoForMonth = (TimeAgoFormatInfo)_infoForWeek.Clone();
			_infoForMonth.UseTimeAgo |= UseTimeAgo.UseMonth;

			_infoForYear = (TimeAgoFormatInfo)_infoForMonth.Clone();
			_infoForYear.UseTimeAgo |= UseTimeAgo.UseYear;

			_infoForDecade = (TimeAgoFormatInfo)_infoForYear.Clone();
			_infoForDecade.UseTimeAgo |= UseTimeAgo.UseDecade;

			_infoForCentury = (TimeAgoFormatInfo)_infoForDecade.Clone();
			_infoForCentury.UseTimeAgo |= UseTimeAgo.UseCentury;
		}

		[Test]
		[TestCaseSource(nameof(TestSecondsCases))]
		public string TestSeconds(int value)
		{
			return TimeSpan.FromSeconds(value).ToShortTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestSecondsCases
			=> TestSecondsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestSecondsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(0, 60).Select(v => new TestCaseData(v).Returns($"{v}s"));
				yield return Enumerable.Range(60, (60 - 1) * 60)
					.Where(v => v % 60 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60}m"));
				yield return Enumerable.Range(60, (60 - 1) * 60)
					.Where(v => v % 60 != 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60}m {v % 60}s"));
#if HEAVY_TEST
				yield return Enumerable.Range(60 * 60, (24 - 1) * 60 * 60)
					.Where(v => (v / 60) % 60 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / (60 * 60)}h"));
				yield return Enumerable.Range(60 * 60, (24 - 1) * 60 * 60)
					.Where(v => (v / 60) % 60 != 0)
					.Select(v => new TestCaseData(v).Returns($"{v / (60 * 60)}h {(v / 60) % 60}m"));
				yield return Enumerable.Range(24 * 60 * 60, (7 - 1) * 24 * 60 * 60)
					.Where(v => (v / 60 / 60) % 24 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / (24 * 60 * 60)}d"));
				yield return Enumerable.Range(24 * 60 * 60, (7 - 1) * 24 * 60 * 60)
					.Where(v => (v / 60 / 60) % 24 != 0)
					.Select(v => new TestCaseData(v).Returns($"{v / (24 * 60 * 60)}d {(v / 60 / 60) % 24}h"));
#endif
			}
		}

		[Test]
		[TestCaseSource(nameof(TestMinutesCases))]
		public string TestMinutes(int value)
		{
			return TimeSpan.FromMinutes(value).ToShortTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestMinutesCases
			=> TestMinutesCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestMinutesCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(60, (24 - 1) * 60)
					.Where(v => v % 60 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60}h"));
				yield return Enumerable.Range(60, (24 - 1) * 60)
					.Where(v => v % 60 != 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60}h {v % 60}m"));
			}
		}

		[Test]
		[TestCaseSource(nameof(TestHoursCases))]
		public string TestHours(int value)
		{
			return TimeSpan.FromHours(value).ToShortTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestHoursCases
			=> TestHoursCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestHoursCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(24, (7 - 1) * 24)
					.Where(v => v % 24 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 24}d"));
				yield return Enumerable.Range(24, (7 - 1) * 24)
					.Where(v => v % 24 != 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 24}d {v % 24}h"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestDaysCases))]
		public string TestDays(int value)
		{
			return TimeSpan.FromDays(value).ToShortTimeAgo(_infoForWeek);
		}
		public static IEnumerable<TestCaseData> TestDaysCases
			=> TestDaysCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestDaysCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(7, (30 - 7) + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 7}w"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestMonthsCases))]
		public string TestMonths(int value)
		{
			return TimeSpan.FromDays(value).ToShortTimeAgo(_infoForMonth);
		}
		public static IEnumerable<TestCaseData> TestMonthsCases
			=> TestMonthsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestMonthsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(30, 365 - 30 + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 30}mn"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestYearsCases))]
		public string TestYears(int value)
		{
			return TimeSpan.FromDays(value).ToShortTimeAgo(_infoForYear);
		}
		public static IEnumerable<TestCaseData> TestYearsCases
			=> TestYearsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestYearsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(365, 10 * (365) + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 365}y"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestDecadesCases))]
		public string TestDecades(int value)
		{
			return TimeSpan.FromDays(value).ToShortTimeAgo(_infoForDecade);
		}
		public static IEnumerable<TestCaseData> TestDecadesCases
			=> TestDecadesCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestDecadesCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(1, 10 + 200 /* ext */)
					.Select(v => new TestCaseData(v * (4 * 365 + 2 * 366)).Returns($"{v}0y"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestCenturysCases))]
		public string TestCenturys(int value)
		{
			return TimeSpan.FromDays(value).ToShortTimeAgo(_infoForCentury);
		}
		public static IEnumerable<TestCaseData> TestCenturysCases
			=> TestCenturysCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestCenturysCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(1, 100 + 50 /* ext */)
					.Select(v => new TestCaseData(v * (75 * 365 + 25 * 366)).Returns($"{v}c"));

			}
		}
	}
}
