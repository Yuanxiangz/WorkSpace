using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Data;
using Problem1.Interface;

namespace Problem1.Caculator
{
	public class CaculatorFactory
	{
		public static ICaculator Create(QuestionType qType)
		{
			switch (qType)
			{
				case QuestionType.Distance:
					return new DistanceCaculator();
				case QuestionType.StartEndMaxStops:
					return new StartEndMaxStopsCaculator();
				case QuestionType.StartEndExactStops:
					return new StartEndExactStopsCaculator();
				case QuestionType.StartEndShortest:
					return new StartEndShortestCaculator();
				default:
					return null;
			}
		}
	}
}
