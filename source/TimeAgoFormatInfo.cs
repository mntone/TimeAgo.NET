using System;
using System.Globalization;
using System.Runtime.Serialization;
using TimeAgo.Internal;

namespace TimeAgo.Globalization
{
	[Serializable]
	public sealed partial class TimeAgoFormatInfo : ICloneable, IFormatProvider
	{
		private const string INVALID_OPERATION_READONLY = "IsReadOnly = true";

		private static volatile TimeAgoFormatInfo _invariantInfo;
		private static volatile TimeAgoFormatInfo _cultureInfo;

		[NonSerialized]
		private TimeAgoData _timeAgoData;

		public bool IsReadOnly { get; private set; }

		public TimeAgoFormatInfo()
			: this(TimeAgoData.InvariantCulture)
		{ }

		internal TimeAgoFormatInfo(TimeAgoData data)
			=> _timeAgoData = data;

		public object Clone()
		{
			var n = (TimeAgoFormatInfo)MemberwiseClone();
			n.IsReadOnly = false;
			return n;
		}

		public object GetFormat(Type formatType)
			=> formatType == typeof(TimeAgoFormatInfo) ? this : null;

		internal ITimeAgoFormatDelegate Short => new ShortTimeAgoFormatDelegate(this);
		internal ITimeAgoFormatDelegate Medium => new MediumTimeAgoFormatDelegate(this);
		internal ITimeAgoFormatDelegate Long => new LongTimeAgoFormatDelegate(this);

		#region Serialization

		private bool IsInvariant { get; set; }

		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (IsInvariant)
				_timeAgoData = TimeAgoData.InvariantCulture;
			else
				_timeAgoData = TimeAgoData.CurrentCulture;
		}

		#endregion

		public UseTimeAgo UseTimeAgo
		{
			get => _UseTimeAgo ?? _timeAgoData.DefaultUseTimeAgo;
			set
			{
				if (IsReadOnly) throw new InvalidOperationException(INVALID_OPERATION_READONLY);
				_UseTimeAgo = value;
			}
		}
		private UseTimeAgo? _UseTimeAgo;

		internal CultureInfo Culture
			=> _timeAgoData.Culture;

		public static TimeAgoFormatInfo InvariantInfo
		{
			get
			{
				if (_invariantInfo == null)
				{
					var invariantInfo = new TimeAgoFormatInfo(TimeAgoData.InvariantCulture)
					{
						IsReadOnly = true,
						IsInvariant = true,
					};
					_invariantInfo = invariantInfo;
				}
				return _invariantInfo;
			}
		}

		public static TimeAgoFormatInfo CultureInfo
		{
			get
			{
				if (_cultureInfo == null)
				{
					var cultureInfo = new TimeAgoFormatInfo(TimeAgoData.CurrentCulture)
					{
						IsReadOnly = true,
						IsInvariant = false,
					};
					_cultureInfo = cultureInfo;
				}
				return _cultureInfo;
			}
		}
	}
}
