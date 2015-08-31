using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eze.Ims.Automation.Framework.Common;
using Eze.Ims.DataServices.Automation;

namespace ConsoleApplication4
{
	class Program
	{
		static void Main(string[] args)
		{
			FluentApiDataSetup fluentApiDataSetup = new FluentApiDataSetup();
			UserIdentity testUser = new UserIdentity()
			{
				FirmAuthId = "THR51",
				FirmName = "AutoTestFirmDSTestK73G91I2",
				FirmId = 52,
				Name = "DSUser",
				Password = "123456"
			};
			User.SetUser(testUser);

			Console.ReadKey();
			fluentApiDataSetup.SaveDefaultSystemMigration();
		}
	}
}
