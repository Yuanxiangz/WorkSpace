using System;
using System.Collections.Generic;
using Eze.Ims.Automation.Framework.Common;
using Eze.Ims.Automation.Framework.Data;
using Eze.Ims.Platform.Api;
using Eze.Ims.Platform.Communication.Interface;

namespace DBMigaration
{
	public class TradingConfigDataPrivider : IImsDataProvider
	{
		private ConfigDataTransportProvider configDataProvider;

		private readonly string testFirm = "TestFirm";
		private readonly string imsRoleTemplate = "TestRole";
		private readonly string testUser = "user";
		private readonly string admin = "admin";
		private readonly string operationUser = "opuser";

		private Dictionary<string, List<string>> roleUsers = new Dictionary<string, List<string>>();

		public TradingConfigDataPrivider()
		{
			configDataProvider = new ConfigDataTransportProvider();

			//tenant firm role template
			roleUsers.Add(TestCommon.FirmAdministratorRole, new List<string>() { admin });
			roleUsers.Add(TestCommon.OperationsRole, new List<string>() { operationUser });
			roleUsers.Add(imsRoleTemplate, new List<string>() { testUser });
		}

		public List<FirmDataTransport> GetFirmDataList()
		{
			List<FirmDataTransport> firmList = new List<FirmDataTransport>();
			firmList.Add(configDataProvider.GetFirmData(testFirm));

			return firmList;
		}

		public List<RoleTransport> GetRoleList(string firmName)
		{
			return null;
		}

		public List<RoleTemplateTransport> GetRoleTemplateDataList()
		{
			List<RoleTemplateTransport> rtList = new List<RoleTemplateTransport>();

			//nliu: Note all data permissions are saved in table: Workflow. SP: pr_SelectWorkflows. The workflow values in the shared class below are no longer used.

			//data permissions
			List<Guid> fullPermissions = new List<Guid>();
			fullPermissions.Add(Workflows.DataLibraryUsersCreate);
			fullPermissions.Add(Workflows.DataLibraryUsersRead);
			fullPermissions.Add(Workflows.DataLibraryUsersUpdate);
			fullPermissions.Add(Workflows.DataLibraryUsersDelete);
			fullPermissions.Add(Workflows.DataLibraryRolesCreate);
			fullPermissions.Add(Workflows.DataLibraryRolesRead);
			fullPermissions.Add(Workflows.DataLibraryRolesUpdate);
			fullPermissions.Add(Workflows.DataLibraryRolesDelete);
			fullPermissions.Add(Workflows.DataLibraryUserCollectionsCreate);
			fullPermissions.Add(Workflows.DataLibraryUserCollectionsRead);
			fullPermissions.Add(Workflows.DataLibraryUserCollectionsUpdate);
			fullPermissions.Add(Workflows.DataLibraryUserCollectionsDelete);

			//workflow permissions
			fullPermissions.Add(Workflows.DesktopSetup);
			fullPermissions.Add(Workflows.DsClientConnectionAdministration);
			fullPermissions.Add(Workflows.DsEsgConnectionAdministration);
			fullPermissions.Add(Workflows.DsInterfaceSupport);
			fullPermissions.Add(new Guid("343CA774-FCC6-43BE-9442-1F4126938A35"));//Enable Tiles

			var rt = configDataProvider.GetRoleTemplateData(imsRoleTemplate, fullPermissions);
			rtList.Add(rt);

			return rtList;
		}

		public List<UserData> GetRoleUserList(string firmName, string roleName)
		{
			List<UserData> userList = null;
			if (firmName.Equals(testFirm) && roleUsers.ContainsKey(roleName))
			{
				userList = new List<UserData>();
				roleUsers[roleName].ForEach(u =>
				{
					userList.Add(configDataProvider.GetUserData(u));
				});
			}

			return userList;
		}

		public List<UserCollectionTransport> GetUserCollectionDataList(string firmName)
		{
			return null;
		}

		public List<UserData> GetUserDataList(string firmName)
		{
			var userList = new List<UserData>();
			userList.Add(configDataProvider.GetUserData(admin));
			return userList;
		}
	}
}
