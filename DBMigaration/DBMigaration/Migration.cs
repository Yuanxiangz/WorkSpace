using System;
using Eze.Ims.Automation.Framework.Common;
using Eze.Ims.Automation.Framework.Data;
using Eze.IMSDS.APITests.Shared.Common;
using Eze.IMSDS.APITests.Shared.Operations;

namespace DBMigaration
{
	public class Migration
	{
		public static void DoMigration()
		{
			Console.WriteLine("Setup tenant DB");
			////IImsDataInitialization imsDataInitialization = new ImsTradingDataInitialization();
			////imsDataInitialization.Initialize();

			////UserIdentity userIdentity = null;
			////if (GlobalDataCache.Instance.FirmDataList != null && GlobalDataCache.Instance.FirmDataList.Count > 1)
			////{
			////	foreach (var firm in GlobalDataCache.Instance.FirmDataList)
			////	{
			////		if (firm.Firm.FirmName == Constants.TestFirm)
			////		{
			////			if (firm.UserList != null && firm.UserList.Count > 0)
			////			{
			////				userIdentity = new UserIdentity();
			////				userIdentity.FirmName = firm.Firm.FirmName;
			////				userIdentity.FirmId = firm.Firm.Id;
			////				userIdentity.Name = firm.UserList[0].UserName;
			////				userIdentity.Password = "123";
			////			}

			////			break;
			////		}
			////	}
			////}

			IImsDataProvider dataProvider = new TradingConfigDataPrivider();
			TenantDataRepository specificTenantRepo = new TenantDataRepository(dataProvider);
			specificTenantRepo.Setup();
			UserIdentity userIdentity = DSOperation.GetUserIdentity(dataProvider);

			Console.WriteLine("Migarate");
			DataSyncManager dataSyncMgr = new DataSyncManager(userIdentity);
			ImsSetupManager imsSetupManager = new ImsSetupManager(userIdentity);
			imsSetupManager.InitializeAdditionalTenantConfiguration();
			dataSyncMgr.StartImsAdapter();

			Console.WriteLine("clean TC table and import basic data");
			OmsOperation omsOperation = new OmsOperation(ConfigSettings.OmsDbConnStr);
			omsOperation.RefreshData();
			omsOperation.ImportBasicOmsData();

			Console.WriteLine("data sync");
			dataSyncMgr.DetectSyncFinish(() =>
			{
				imsSetupManager.UpdateTenantDbToTriggerResync(userIdentity.FirmName);
			});

			DSOperation.ValidateMinimumRecordCount(userIdentity.FirmName, 0, "ds_stg.tr_sec");
			DSOperation.ValidateMinimumRecordCount(userIdentity.FirmName, 0, "ds_stg.tc_sectype");
			DSOperation.ValidateMinimumRecordCount(userIdentity.FirmName, 0, "ds_stg.ec_deftype");

			imsSetupManager.SyncData();
			DSOperation.ValidateMinimumRecordCount(userIdentity.FirmName, 0, "Security");
			Console.WriteLine("Finished!!!");
		}
	}
}
