using System;
using System.Configuration;
using Problem2.Data;

namespace Problem2.Config
{
	public static class ConfigurationSetting
	{
		#region Private Member
		private static TimeSchduleStrategy strategy;
		private static int morningDuration;
		private static int afternoonDuration;
		private static string inputFile;
		private static DateTime morningStartTime;
		private static DateTime afternoonStartTime;
		#endregion

		#region Properties
		public static TimeSchduleStrategy Strategy { get { return strategy; } }
		public static DateTime MorningStartTime { get { return morningStartTime; } }
		public static int MorningDuration { get { return morningDuration; } }
		public static DateTime AfternoonStartTime { get { return afternoonStartTime; } }
		public static int AfternoonDuration { get { return afternoonDuration; } }
		public static string InputFile { get { return inputFile; } }
		#endregion

		public static void Initialize()
		{
			strategy = ConvertEnum<TimeSchduleStrategy>(ConfigurationSettings.AppSettings["Strategy"].ToString(), true);
			int.TryParse(ConfigurationSettings.AppSettings["MorningDuration"].ToString(), out morningDuration);
			int.TryParse(ConfigurationSettings.AppSettings["AfternoonDuration"].ToString(), out afternoonDuration);
			morningStartTime = DateTime.Parse(ConfigurationSettings.AppSettings["MorningStartTime"].ToString());
			afternoonStartTime = DateTime.Parse(ConfigurationSettings.AppSettings["AfternoonStartTime"].ToString());
			inputFile = ConfigurationSettings.AppSettings["InputFile"].ToString();
		}

		#region Private Method
		private static T ConvertEnum<T>(string input, bool ignoreCase = false) where T : struct
		{
			T s;
			if (Enum.TryParse(input, ignoreCase, out s))
			{
				return s;
			}
			return default(T);
		}
		#endregion
	}
}
