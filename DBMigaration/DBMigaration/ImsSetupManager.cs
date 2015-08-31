using Eze.Ims.Automation.Framework.Common;
using Eze.IMSDS.APITests.Shared.Common;
using Eze.IMSDS.APITests.Shared.Operations;

namespace DBMigaration
{
	public interface IImsSetupManager
	{
		void InitializeAdditionalTenantConfiguration();

		void UpdateTenantDbToTriggerResync(string firmName);
	}

	public class ImsSetupManager : IImsSetupManager
	{
		private readonly ImsDbOperation _imsDbOperation;
		private readonly UserIdentity _userIdentity;
		private readonly DbOperation _dbOperation;

		private readonly string Table_Staging_SecurityMaster = System.IO.Path.Combine(ConfigSettings.GetWorkingDirectory(), ConstantString.SqlPath, "Staging_SecurityMaster.sql");
		private readonly string SP_ETL_UpdateSecurity_AssetType = System.IO.Path.Combine(ConfigSettings.GetWorkingDirectory(), ConstantString.SqlPath, "pr_ETL_UpdateSecurity_AssetType.sql");
		private readonly string SP_ETL_SecurityCashSync = System.IO.Path.Combine(ConfigSettings.GetWorkingDirectory(), ConstantString.SqlPath, "pr_ETL_SecurityCashSync.sql");
		private readonly string SP_ETL_SecurityMasterSync = System.IO.Path.Combine(ConfigSettings.GetWorkingDirectory(), ConstantString.SqlPath, "pr_ETL_SecurityMasterSync.sql");

		public ImsSetupManager(UserIdentity userIdentity)
		{
			_userIdentity = userIdentity;
			_imsDbOperation = new ImsDbOperation();
			_dbOperation = new DbOperation(_imsDbOperation.GetTenantConnectionStr(_userIdentity.FirmName));
		}

		public void InitializeAdditionalTenantConfiguration()
		{
			_imsDbOperation.UpdateDsSettings(_userIdentity.FirmName);
			_dbOperation.ExecuteNonQuery("update IntegrationSettings set Settingvalue='Enabled'");
			_imsDbOperation.ResetSecurityMasterSyncTimestamp(_userIdentity.FirmName);
		}

		public void UpdateTenantDbToTriggerResync(string firmName)
		{
			_imsDbOperation.TriggerResync(_userIdentity.FirmName);
		}

		public void SyncData()
		{
			_dbOperation.RunSQLInCommandFile(Table_Staging_SecurityMaster);
			_dbOperation.RunSQLInCommandFile(SP_ETL_UpdateSecurity_AssetType);
			_dbOperation.RunSQLInCommandFile(SP_ETL_SecurityCashSync);
			_dbOperation.RunSQLInCommandFile(SP_ETL_SecurityMasterSync);
			_dbOperation.ExecuteNonQuery("exec pr_ETL_SecurityMasterSync");

			// Move reference data to IMS
			_dbOperation.ExecuteNonQuery("exec pr_MoveOMSData_ReferenceData");
		}
	}
}
