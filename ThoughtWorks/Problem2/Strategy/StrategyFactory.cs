using Problem2.Config;
using Problem2.Data;
using Problem2.Interface;

namespace Problem2.Strategy
{
	public class StrategyFactory
	{
		public IStrategy Create()
		{
			switch (ConfigurationSetting.Strategy)
			{
				case TimeSchduleStrategy.EfficiencyFirst:
					return new EfficiencyFirstStrategy();
				case TimeSchduleStrategy.TimeUtilizitionFirst:
					return new TimeUtilizitionFirstStrategy();
				default:
					return new EfficiencyFirstStrategy();
			}
		}
	}
}
