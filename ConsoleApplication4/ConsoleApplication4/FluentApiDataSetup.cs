using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eze.Ims.DataServices.Automation.Common;
using Eze.Ims.DataServices.Automation.SystemMigration;

namespace ConsoleApplication4
{
	public class FluentApiDataSetup
	{
		public SystemMigration SaveDefaultSystemMigration()
		{
			var systemMigration = new SystemMigration();
			systemMigration
				.Create<AssetClassDefinitions>(a => a
					.With(SecurityTypes.Abs)
					.When(Field.TrSec_SecSym)
					.Equals("IBM"))
				.Create<AssetClassDefinitions>(a => a
					.With(SecurityTypes.Adr)
					.When(Field.TrSec_SecSym)
					.Equals("DELL"))
				.Create<AssetClassDefinitions>(a => a
					.With(SecurityTypes.Cmo)
					.When(Field.TrSec_SecSym)
					.Equals("QQQ AAA"))
				.Create<SecurityIdentifiers>(s => s
					.Set(SecurityDestinations.Symbol, SecuritySources.TrSec_Sec_SYM)
					.Set(SecurityDestinations.Alias1, SecuritySources.TrSec_Sec_Alias1))
				.Create<CustodialDefinition>(c => c
					.Set(CustodialDestinations.Portfolio, CustodialSources.TcHedgeware_Porfolio)
					.Set(CustodialDestinations.Custodian, CustodialSources.TcHedgeware_Cust)
					.Set(CustodialDestinations.Custodial_Account, CustodialSources.TcHedgeware_Account))
				.Save();
			return systemMigration;
		}
	}
}
