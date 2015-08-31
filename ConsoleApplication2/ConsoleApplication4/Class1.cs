using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
	public class Class1
	{
		public async Task<int> GetInt()
		{
			return await GetResult();
		}

		public Task<int> GetResult()
		{
			Task.Delay(2000);

			return new Task<int>(() => { return 1; });
		}

		public async void Go()
		{
			int a = await GetInt();
			int b = 2 + a;
		}
	}
}
