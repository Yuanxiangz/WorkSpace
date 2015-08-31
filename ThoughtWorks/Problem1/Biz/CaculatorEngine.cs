using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Data;
using Problem1.Interface;

namespace Problem1.Biz
{
	public class CaculatorEngine
	{
		private List<ICaculator> caculatorList = new List<ICaculator>();

		public void AddCaculator(ICaculator caculator)
		{
			caculatorList.Add(caculator);
		}

		public void Caculate()
		{
			int count=0;
			foreach (ICaculator caculator in caculatorList)
			{
				count++;
				int result=caculator.Caculate();
				if(result < 0)
					Console.WriteLine(string.Format(ConstStr.OUTPUTSTR, count, ConstStr.NOSUCHROUTE));
				else
					Console.WriteLine(string.Format(ConstStr.OUTPUTSTR, count, result));
			}
		}
	}
}
