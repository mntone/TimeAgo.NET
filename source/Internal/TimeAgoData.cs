using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace TimeAgo.Internal
{
	internal class TimeAgoData
    {
		internal UseTimeAgo DefaultUseTimeAgo
			= UseTimeAgo.UseNow
			| UseTimeAgo.UseSecond
			| UseTimeAgo.UseMinute
			| UseTimeAgo.UseHour
			| UseTimeAgo.UseDay
			| UseTimeAgo.UseComplex;
		internal CultureInfo Culture;

		internal int Switch_Now_Seconds;

		internal string Now_Medium;
		internal string Now_Long;

		internal string Seconds_Short;
		internal string Seconds_Medium;
		internal string Seconds_Medium_One;
		internal string Seconds_Long;
		internal string Seconds_Long_One;

		internal string Minutes_Seconds_Short;
		internal string Minutes_Seconds_Medium;
		internal string Minutes_Seconds_Medium_MOne;
		internal string Minutes_Seconds_Medium_SOne;
		internal string Minutes_Seconds_Medium_MSOne;
		internal string Minutes_Seconds_Long;
		internal string Minutes_Seconds_Long_MOne;
		internal string Minutes_Seconds_Long_SOne;
		internal string Minutes_Seconds_Long_MSOne;

		internal string Minutes_Short;
		internal string Minutes_Medium;
		internal string Minutes_Medium_One;
		internal string Minutes_Long;
		internal string Minutes_Long_One;

		internal string Hours_Minutes_Short;
		internal string Hours_Minutes_Medium;
		internal string Hours_Minutes_Medium_HOne;
		internal string Hours_Minutes_Medium_MOne;
		internal string Hours_Minutes_Medium_HMOne;
		internal string Hours_Minutes_Long;
		internal string Hours_Minutes_Long_HOne;
		internal string Hours_Minutes_Long_MOne;
		internal string Hours_Minutes_Long_HMOne;

		internal string Hours_Short;
		internal string Hours_Medium;
		internal string Hours_Medium_One;
		internal string Hours_Long_One;
		internal string Hours_Long;

		internal string Days_Hours_Short;
		internal string Days_Hours_Medium;
		internal string Days_Hours_Medium_DOne;
		internal string Days_Hours_Medium_HOne;
		internal string Days_Hours_Medium_DHOne;
		internal string Days_Hours_Long;
		internal string Days_Hours_Long_DOne;
		internal string Days_Hours_Long_HOne;
		internal string Days_Hours_Long_DHOne;

		internal string Days_Short;
		internal string Days_Medium;
		internal string Days_Medium_One;
		internal string Days_Long;
		internal string Days_Long_One;

		internal string Weeks_Short;
		internal string Weeks_Medium;
		internal string Weeks_Medium_One;
		internal string Weeks_Long;
		internal string Weeks_Long_One;

		internal string Months_Short;
		internal string Months_Medium;
		internal string Months_Medium_One;
		internal string Months_Long;
		internal string Months_Long_One;

		internal string Years_Short;
		internal string Years_Medium;
		internal string Years_Medium_One;
		internal string Years_Long;
		internal string Years_Long_One;

		internal string Decades_Short;
		internal string Decades_Medium;
		internal string Decades_Medium_One;
		internal string Decades_Long;
		internal string Decades_Long_One;

		internal string Centurys_Short;
		internal string Centurys_Medium;
		internal string Centurys_Medium_One;
		internal string Centurys_Long;
		internal string Centurys_Long_One;

		internal static TimeAgoData InvariantCulture
		{
			get
			{
				if (_InvariantCulture == null)
				{
					var culture = CultureInfo.CreateSpecificCulture("en");
					var invariant = Load(culture);
					_InvariantCulture = invariant;
				}
				return _InvariantCulture;
			}
		}
		private static TimeAgoData _InvariantCulture;

		internal static TimeAgoData CurrentCulture
		{
			get
			{
				if (_CurrentCulture == null)
				{
					var culture = Thread.CurrentThread.CurrentCulture;
					var currentCulture = Load(culture);
					_CurrentCulture = currentCulture;
				}
				return _CurrentCulture;
			}
		}
		private static TimeAgoData _CurrentCulture;

		#region Resource Loader

		private static TimeAgoData Load(CultureInfo culture)
		{
			return new TimeAgoData()
			{
				Culture = culture,

				Switch_Now_Seconds = GetObject<int>(nameof(Switch_Now_Seconds), culture),

				Now_Medium = GetString(nameof(Now_Medium), culture),
				Now_Long = GetString(nameof(Now_Long), culture),

				Seconds_Short = GetString(nameof(Seconds_Short), culture),
				Seconds_Medium = GetString(nameof(Seconds_Medium), culture),
				Seconds_Medium_One = GetString(nameof(Seconds_Medium_One), culture),
				Seconds_Long = GetString(nameof(Seconds_Long), culture),
				Seconds_Long_One = GetString(nameof(Seconds_Long_One), culture),

				Minutes_Seconds_Short = GetString(nameof(Minutes_Seconds_Short), culture),
				Minutes_Seconds_Medium = GetString(nameof(Minutes_Seconds_Medium), culture),
				Minutes_Seconds_Medium_MOne = GetString(nameof(Minutes_Seconds_Medium_MOne), culture),
				Minutes_Seconds_Medium_SOne = GetString(nameof(Minutes_Seconds_Medium_SOne), culture),
				Minutes_Seconds_Medium_MSOne = GetString(nameof(Minutes_Seconds_Medium_MSOne), culture),
				Minutes_Seconds_Long = GetString(nameof(Minutes_Seconds_Long), culture),
				Minutes_Seconds_Long_MOne = GetString(nameof(Minutes_Seconds_Long_MOne), culture),
				Minutes_Seconds_Long_SOne = GetString(nameof(Minutes_Seconds_Long_SOne), culture),
				Minutes_Seconds_Long_MSOne = GetString(nameof(Minutes_Seconds_Long_MSOne), culture),

				Minutes_Short = GetString(nameof(Minutes_Short), culture),
				Minutes_Medium = GetString(nameof(Minutes_Medium), culture),
				Minutes_Medium_One = GetString(nameof(Minutes_Medium_One), culture),
				Minutes_Long = GetString(nameof(Minutes_Long), culture),
				Minutes_Long_One = GetString(nameof(Minutes_Long_One), culture),

				Hours_Minutes_Short = GetString(nameof(Hours_Minutes_Short), culture),
				Hours_Minutes_Medium = GetString(nameof(Hours_Minutes_Medium), culture),
				Hours_Minutes_Medium_HOne = GetString(nameof(Hours_Minutes_Medium_HOne), culture),
				Hours_Minutes_Medium_MOne = GetString(nameof(Hours_Minutes_Medium_MOne), culture),
				Hours_Minutes_Medium_HMOne = GetString(nameof(Hours_Minutes_Medium_HMOne), culture),
				Hours_Minutes_Long = GetString(nameof(Hours_Minutes_Long), culture),
				Hours_Minutes_Long_HOne = GetString(nameof(Hours_Minutes_Long_HOne), culture),
				Hours_Minutes_Long_MOne = GetString(nameof(Hours_Minutes_Long_MOne), culture),
				Hours_Minutes_Long_HMOne = GetString(nameof(Hours_Minutes_Long_HMOne), culture),

				Hours_Short = GetString(nameof(Hours_Short), culture),
				Hours_Medium = GetString(nameof(Hours_Medium), culture),
				Hours_Medium_One = GetString(nameof(Hours_Medium_One), culture),
				Hours_Long = GetString(nameof(Hours_Long), culture),
				Hours_Long_One = GetString(nameof(Hours_Long_One), culture),

				Days_Hours_Short = GetString(nameof(Days_Hours_Short), culture),
				Days_Hours_Medium = GetString(nameof(Days_Hours_Medium), culture),
				Days_Hours_Medium_DOne = GetString(nameof(Days_Hours_Medium_DOne), culture),
				Days_Hours_Medium_HOne = GetString(nameof(Days_Hours_Medium_HOne), culture),
				Days_Hours_Medium_DHOne = GetString(nameof(Days_Hours_Medium_DHOne), culture),
				Days_Hours_Long = GetString(nameof(Days_Hours_Long), culture),
				Days_Hours_Long_DOne = GetString(nameof(Days_Hours_Long_DOne), culture),
				Days_Hours_Long_HOne = GetString(nameof(Days_Hours_Long_HOne), culture),
				Days_Hours_Long_DHOne = GetString(nameof(Days_Hours_Long_DHOne), culture),

				Days_Short = GetString(nameof(Days_Short), culture),
				Days_Medium = GetString(nameof(Days_Medium), culture),
				Days_Medium_One = GetString(nameof(Days_Medium_One), culture),
				Days_Long = GetString(nameof(Days_Long), culture),
				Days_Long_One = GetString(nameof(Days_Long_One), culture),

				Weeks_Short = GetString(nameof(Weeks_Short), culture),
				Weeks_Medium = GetString(nameof(Weeks_Medium), culture),
				Weeks_Medium_One = GetString(nameof(Weeks_Medium_One), culture),
				Weeks_Long = GetString(nameof(Weeks_Long), culture),
				Weeks_Long_One = GetString(nameof(Weeks_Long_One), culture),

				Months_Short = GetString(nameof(Months_Short), culture),
				Months_Medium = GetString(nameof(Months_Medium), culture),
				Months_Medium_One = GetString(nameof(Months_Medium_One), culture),
				Months_Long = GetString(nameof(Months_Long), culture),
				Months_Long_One = GetString(nameof(Months_Long_One), culture),

				Years_Short = GetString(nameof(Years_Short), culture),
				Years_Medium = GetString(nameof(Years_Medium), culture),
				Years_Medium_One = GetString(nameof(Years_Medium_One), culture),
				Years_Long = GetString(nameof(Years_Long), culture),
				Years_Long_One = GetString(nameof(Years_Long_One), culture),

				Decades_Short = GetString(nameof(Decades_Short), culture),
				Decades_Medium = GetString(nameof(Decades_Medium), culture),
				Decades_Medium_One = GetString(nameof(Decades_Medium_One), culture),
				Decades_Long = GetString(nameof(Decades_Long), culture),
				Decades_Long_One = GetString(nameof(Decades_Long_One), culture),

				Centurys_Short = GetString(nameof(Centurys_Short), culture),
				Centurys_Medium = GetString(nameof(Centurys_Medium), culture),
				Centurys_Medium_One = GetString(nameof(Centurys_Medium_One), culture),
				Centurys_Long = GetString(nameof(Centurys_Long), culture),
				Centurys_Long_One = GetString(nameof(Centurys_Long_One), culture),
			};
		}

		#endregion

		#region Resource Manager

		internal static volatile ResourceManager _TimeAgoResourceManager;

		private static string GetString(string resourceKey, CultureInfo culture)
		{
			if (_TimeAgoResourceManager == null)
			{
				_TimeAgoResourceManager = new ResourceManager("TimeAgo", typeof(TimeAgoData).GetTypeInfo().Assembly);
			}
			return _TimeAgoResourceManager.GetString(resourceKey, culture);
		}

		private static T GetObject<T>(string resourceKey, CultureInfo culture)
		{
			if (_TimeAgoResourceManager == null)
			{
				_TimeAgoResourceManager = new ResourceManager("TimeAgo", typeof(TimeAgoData).GetTypeInfo().Assembly);
			}
			return (T)_TimeAgoResourceManager.GetObject(resourceKey, culture);
		}

		#endregion
	}
}
