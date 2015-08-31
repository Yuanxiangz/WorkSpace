using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Caculator;
using Problem1.Data;
using Problem1.Interface;
using Problem1.Utils;

namespace Problem1.Biz
{
	public class Problem1WorkflowManager
	{
		private CaculatorEngine caculatorEngine;
		private List<string> questions;

		public Problem1WorkflowManager()
		{
			try
			{
				QuestionMap.LoadMap();
				caculatorEngine = new CaculatorEngine();
				questions = new List<string>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public void Run()
		{
			try
			{
				int count = 0;
				Dictionary<string, QuestionType> questionMap = QuestionMap.GetQuestionMap();
				IList<string> questions = InputLoader.LoadQuestions(ConstStr.QUESTIONFILEPATH);
				IDictionary<MultplePrimKey, int> roads = InputLoader.LoadInputs(ConstStr.INPUTFILEPATH);
				foreach (string key in questionMap.Keys)
				{
					count++;
					ICaculator caculator = CaculatorFactory.Create(questionMap[key]);
					caculator.LoadQuestionAndInput(questions[count - 1], roads);
					caculatorEngine.AddCaculator(caculator);
				}

				caculatorEngine.Caculate();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
