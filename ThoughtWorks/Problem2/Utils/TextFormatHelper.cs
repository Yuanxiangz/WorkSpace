
namespace Problem2.Utils
{
	public static class TextFormatHelper
	{
		public const string LIGHTNING = "lightning";
		public const int LIGHTNING_MINS = 5;
		public const string NETWORKINGEVENTSTARTTIME = "04:00 PM";
		public const string NETWORKINGEVENTSTR = "Networking Event";
		public const string TIMEFORMAT = "hh:mm tt";
		public const string TIMESUFFIX = "min";

		public static int ConvertTimeStrToNum(string time)
		{
			int num = -1;
			if (time.ToLower().Equals(LIGHTNING))
				return num = 5;
			
			if (!int.TryParse(time.Replace(TIMESUFFIX, ""), out num))
				return num = -1;

			return num;
		}
	}
}
