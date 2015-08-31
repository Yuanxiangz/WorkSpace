using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigaration
{
	class Program
	{
		static void Main(string[] args)
		{
			Migration.DoMigration();
			Console.ReadKey();
		}
	}
}
