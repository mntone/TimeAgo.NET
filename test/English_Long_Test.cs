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
	public class English_Long_Test
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
			return TimeSpan.FromSeconds(value).ToLongTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestSecondsCases
			=> TestSecondsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestSecondsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(0, 6)
					.Select(v => new TestCaseData(v).Returns($"Just now"));
				yield return Enumerable.Range(6, 60 - 6)
					.Where(v => v != 1)
					.Select(v => new TestCaseData(v).Returns($"{v} seconds ago"));
				yield return new[] { new TestCaseData(60).Returns($"A minute ago") };
				yield return Enumerable.Range(61, (60 - 1) * 60 - 1)
					.Where(v => v % 60 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} minutes ago"));
				yield return Enumerable.Range(61, (60 - 1) * 60 - 1)
					.Where(v => v / 60 == 1 && v % 60 != 0)
					.Select(v => new TestCaseData(v).Returns($"A minute and {v % 60} seconds ago"));
				yield return Enumerable.Range(61, (60 - 1) * 60 - 1)
					.Where(v => v / 60 != 1 && v % 60 != 0 && v % 60 == 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} minutes and a second ago"));
				yield return Enumerable.Range(61, (60 - 1) * 60 - 1)
					.Where(v => v / 60 != 1 && v % 60 != 0 && v % 60 != 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} minutes and {v % 60} seconds ago"));
			}
		}

		[Test]
		[TestCaseSource(nameof(TestMinutesCases))]
		public string TestMinutes(int value)
		{
			return TimeSpan.FromMinutes(value).ToLongTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestMinutesCases
			=> TestMinutesCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestMinutesCasesDelegate
		{
			get
			{
				yield return new[] { new TestCaseData(60).Returns($"An hour ago") };
				yield return Enumerable.Range(61, (24 - 1) * 60 - 1)
					.Where(v => v % 60 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} hours ago"));
				yield return Enumerable.Range(61, (24 - 1) * 60 - 1)
					.Where(v => v / 60 == 1 && v % 60 != 0)
					.Select(v => new TestCaseData(v).Returns($"An hour and {v % 60} minutes ago"));
				yield return Enumerable.Range(61, (24 - 1) * 60 - 1)
					.Where(v => v / 60 != 1 && v % 60 != 0 && v % 60 == 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} hours and a minute ago"));
				yield return Enumerable.Range(61, (24 - 1) * 60 - 1)
					.Where(v => v / 60 != 1 && v % 60 != 0 && v % 60 != 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 60} hours and {v % 60} minutes ago"));
			}
		}

		[Test]
		[TestCaseSource(nameof(TestHoursCases))]
		public string TestHours(int value)
		{
			return TimeSpan.FromHours(value).ToLongTimeAgo();
		}
		public static IEnumerable<TestCaseData> TestHoursCases
			=> TestHoursCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestHoursCasesDelegate
		{
			get
			{
				yield return new[] { new TestCaseData(24).Returns($"A day ago") };
				yield return Enumerable.Range(25, (7 - 1) * 24 - 1)
					.Where(v => v % 24 == 0)
					.Select(v => new TestCaseData(v).Returns($"{v / 24} days ago"));
				yield return Enumerable.Range(25, (7 - 1) * 24 - 1)
					.Where(v => v / 24 == 1 && v % 24 != 0)
					.Select(v => new TestCaseData(v).Returns($"A day and {v % 24} hours ago"));
				yield return Enumerable.Range(25, (7 - 1) * 24 - 1)
					.Where(v => v / 24 != 1 && v % 24 != 0 && v % 24 == 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 24} days and an hour ago"));
				yield return Enumerable.Range(25, (7 - 1) * 24 - 1)
					.Where(v => v / 24 != 1 && v % 24 != 0 && v % 24 != 1)
					.Select(v => new TestCaseData(v).Returns($"{v / 24} days and {v % 24} hours ago"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestDaysCases))]
		public string TestDays(int value)
		{
			return TimeSpan.FromDays(value).ToLongTimeAgo(_infoForWeek);
		}
		public static IEnumerable<TestCaseData> TestDaysCases
			=> TestDaysCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestDaysCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(7, 7)
					.Select(v => new TestCaseData(v).Returns($"A week ago"));
				yield return Enumerable.Range(14, (30 - 7) + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 7} weeks ago"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestMonthsCases))]
		public string TestMonths(int value)
		{
			return TimeSpan.FromDays(value).ToLongTimeAgo(_infoForMonth);
		}
		public static IEnumerable<TestCaseData> TestMonthsCases
			=> TestMonthsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestMonthsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(30, 30)
					.Select(v => new TestCaseData(v).Returns($"A month ago"));
				yield return Enumerable.Range(60, 365 - 30 - 30 + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 30} months ago"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestYearsCases))]
		public string TestYears(int value)
		{
			return TimeSpan.FromDays(value).ToLongTimeAgo(_infoForYear);
		}
		public static IEnumerable<TestCaseData> TestYearsCases
			=> TestYearsCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestYearsCasesDelegate
		{
			get
			{
				yield return Enumerable.Range(365, 365)
					.Select(v => new TestCaseData(v).Returns($"A year ago"));
				yield return Enumerable.Range(2 * 365, (10 - 1) * (365) + 200 /* ext */)
					.Select(v => new TestCaseData(v).Returns($"{v / 365} years ago"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestDecadesCases))]
		public string TestDecades(int value)
		{
			return TimeSpan.FromDays(value).ToLongTimeAgo(_infoForDecade);
		}
		public static IEnumerable<TestCaseData> TestDecadesCases
			=> TestDecadesCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestDecadesCasesDelegate
		{
			get
			{
				yield return new[] { new TestCaseData(1 * (4 * 365 + 2 * 366)).Returns($"A decade ago") };
				yield return Enumerable.Range(2, (10 - 1) + 200 /* ext */)
					.Select(v => new TestCaseData(v * (4 * 365 + 2 * 366)).Returns($"{v} decades ago"));

			}
		}

		[Test]
		[TestCaseSource(nameof(TestCenturysCases))]
		public string TestCenturys(int value)
		{
			return TimeSpan.FromDays(value).ToLongTimeAgo(_infoForCentury);
		}
		public static IEnumerable<TestCaseData> TestCenturysCases
			=> TestCenturysCasesDelegate.SelectMany(v => v);
		public static IEnumerable<IEnumerable<TestCaseData>> TestCenturysCasesDelegate
		{
			get
			{
				yield return new[] { new TestCaseData(1 * (75 * 365 + 25 * 366)).Returns("A century ago") };
				yield return Enumerable.Range(2, 100 + 50 /* ext */)
					.Select(v => new TestCaseData(v * (75 * 365 + 25 * 366)).Returns($"{v} centurys ago"));

			}
		}
	}
}
