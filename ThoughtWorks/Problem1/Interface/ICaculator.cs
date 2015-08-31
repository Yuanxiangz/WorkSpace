using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Data;

namespace Problem1.Interface
{
	public interface ICaculator
	{
		int Caculate();

		void LoadQuestionAndInput(string question, IDictionary<MultplePrimKey, int> input);
	}
}
