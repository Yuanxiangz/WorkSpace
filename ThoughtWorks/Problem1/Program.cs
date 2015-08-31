using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Biz;

namespace Problem1
{
	class Program
	{
		static void Main(string[] args)
		{
			Problem1WorkflowManager proble1Manager = new Problem1WorkflowManager();
			proble1Manager.Run();

			Console.ReadKey();
		}
	}
}
