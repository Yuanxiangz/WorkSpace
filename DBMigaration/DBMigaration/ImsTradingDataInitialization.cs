using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Eze.Ims.Automation.Framework.Common;
using Eze.Ims.Automation.Framework.Data;
using Eze.Ims.Automation.Framework.Operations;
using Eze.Ims.Automation.Framework.UserSession;
using Eze.Ims.Platform.Api;

namespace DBMigaration
{
	internal class ImsTradingDataInitialization : IImsDataInitialization
	{
		private TestLog log = new TestLog();
		private ImsOperation imsOperation;
		private ImsUserOperation userOperation;

		public ImsTradingDataInitialization(IImsDataProvider provider)
		{
			this.DataProvider = provider;
		}

		public ImsTradingDataInitialization()
			: this(DefaultDataProvider.Instance)
		{
		}

		public IImsDataProvider DataProvider { get; set; }

		public bool Initialize()
		{
			var result = false;

			UserSessionMgr.Instance.RunUnderUserSession(TestCommon.EzeAdminUser,
				provider =>
				{
					imsOperation = new ImsOperation(provider);
					userOperation = new ImsUserOperation(provider);

					var testFirm = GetOrCreateTestFirm(Constants.TestFirm);
					var testRoleTemplate = GetOrCreateTestExternalRole();
					var testUser = GetOrCreateTestUserData(testFirm.Id, testFirm.FirmName);

					if (testFirm != null && testRoleTemplate != null && testUser != null)
					{
						log.Info("Firm, RoleTemplate and User are ready.");
						if (!testFirm.RoleTemplates.Contains(testRoleTemplate.Id))
						{
							testFirm.RoleTemplates.Add(testRoleTemplate.Id);
						}

						testFirm.RoleTemplateUsers.Add(new RoleTemplateUserTransport(testRoleTemplate.Id, testUser.UserId));
						testFirm = UpdateFirm(testFirm);
						log.Info(string.Format("Firm {0} is updated", testFirm.FirmName));

						if (testFirm != null)
						{
							FirmData firm = new FirmData();
							firm.Firm = testFirm;
							firm.RoleTemplateList = new List<RoleTemplateTransport>() { testRoleTemplate };
							firm.UserList = new List<UserData>() { testUser };

							GlobalDataCache.Instance.FirmDataList.Add(firm);
							GlobalDataCache.Instance.RoleTemplateList.Add(testRoleTemplate);
							log.Info(string.Format("Firm {0} and User {1} are stored in GlobalDataCache", firm.Firm.FirmName, testUser.UserName));
							result = true;
						}
					}
				});

			return result;
		}

		private FirmDataTransport GetOrCreateTestFirm(string firmName)
		{
			FirmDataTransport existingFirm = null;
			var response = imsOperation.FirmOperation(Operation.Read);
			if (response != null && response.FirmData != null)
			{
				foreach (var resultFirm in response.FirmData)
				{
					if (resultFirm.FirmName == firmName)
					{
						existingFirm = resultFirm;
						break;
					}
				}
			}

			return existingFirm ?? CreateTestFirm();
		}

		private FirmDataTransport CreateTestFirm()
		{
			var firm = new FirmDataTransport()
			{
				CreatedBy = "eze",
				CreatedDate = DateTime.Now,
				FirmLegalName = Constants.TestFirm,
				FirmName = Constants.TestFirm,
				BillingAddress = "Farnsworth St, Boston",
				DeskLocation = "Farnsworth St, Boston",
				PrimaryContactName = "eze",
				PrimaryContactEmail = TestSettings.EmailAddress,
				PrimaryContactPhone = "123456",
				PrimaryPreferredContact = ContactMethod.Email,
				SecondaryContactName = "eze1",
				SecondaryContactEmail = TestSettings.EmailAddress,
				SecondaryContactPhone = "987654",
				SecondaryPreferredContact = ContactMethod.Phone,
				Seats = 1000,
				ModifiedBy = "eze",
				ModifiedDate = DateTime.Now,
				BillingStartDate = DateTime.Now,
				RegionId = 1
			};

			var response = imsOperation.FirmOperation(Operation.Create, firm);
			if (response != null && response.FirmData != null && response.FirmData.Count == 1)
			{
				log.Info(string.Format("Firm {0} is created.", response.FirmData[0].FirmName));
				InsertDataToTestFirmDB(response.FirmData[0].Id);
				return response.FirmData[0];
			}
			else
			{
				log.Info("Failed to create firm.");
				return null;
			}
		}

		private FirmDataTransport UpdateFirm(FirmDataTransport firm)
		{
			var response = imsOperation.FirmOperation(Operation.Update, firm);

			if (response != null && response.FirmData != null && response.FirmData.Count == 1)
			{
				return response.FirmData.First();
			}
			else
			{
				return null;
			}
		}

		private RoleTemplateTransport GetOrCreateTestExternalRole()
		{
			RoleTemplateTransport existingRole = null;
			var response = imsOperation.RoleTemplateOperation(Operation.Read);
			if (response != null && response.RoleTemplateData != null)
			{
				foreach (var role in response.RoleTemplateData)
				{
					if (role.Name == Constants.TestRole)
					{
						existingRole = role;
						break;
					}
				}
			}

			return existingRole ?? CreateTestExternalRole();
		}

		private RoleTemplateTransport CreateTestExternalRole()
		{
			var roleTemplate = new RoleTemplateTransport();
			roleTemplate.Name = Constants.TestRole;
			roleTemplate.Workflows.AddRange(GetRequiredPermissions());

			var response = imsOperation.RoleTemplateOperation(Operation.Create, roleTemplate);
			if (response != null && response.RoleTemplateData != null && response.RoleTemplateData.Count == 1)
			{
				log.Info(string.Format("External role {0} is created.", response.RoleTemplateData.First().Name));
				return response.RoleTemplateData.First();
			}
			else
			{
				log.Info("Failed to create external Role");
				return null;
			}
		}

		private UserData GetOrCreateTestUserData(int firmId, string firmName)
		{
			UserData existingUser = null;

			var response = userOperation.Read(firmId);
			if (response != null)
			{
				foreach (var user in response)
				{
					if (user.UserName == Constants.TestUser)
					{
						existingUser = user;
						break;
					}
				}
			}

			return existingUser ?? CreateTestUserData(firmId, firmName);
		}

		private UserData CreateTestUserData(int firmId, string firmName)
		{
			var user = new UserData()
			{
				UserId = 0,
				FirmId = firmId,
				Active = true,
				UserName = Constants.TestUser,
				FirstName = Constants.TestUser,
				LastName = Constants.TestUser,
				Position = "AutoQA",
				EmailAddress = Eze.Ims.Automation.Framework.Common.TestSettings.EmailAddress,
				MiddleName = string.Empty,
				Address = string.Empty
			};

			var response = userOperation.AddNew(user);

			if (response != null)
			{
				var newUserIdentity = new UserIdentity()
				{
					Name = response.UserName,
					Password = response.Password,
					FirmId = firmId,
					FirmName = firmName
				};

				// change the password and do nothing else
				UserSessionMgr.Instance.RunUnderNewUserSession(newUserIdentity,
					(p, authId) =>
					{
						log.Info(string.Format("Changed password for user {0}.", newUserIdentity.Name));
					});
			}

			return response;
		}

		private void InsertDataToTestFirmDB(int firmId)
		{
		}

		private string GetWorkingDirectory()
		{
			return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Support");
		}

		private IEnumerable<Guid> GetRequiredPermissions()
		{
			yield return new Guid(Permissions.WorkflowIMSIBORIBORUser);
			yield return new Guid(Permissions.WorkflowIMSDataServicesClientConnectionAdministration);
			yield return new Guid(Permissions.WorkflowIMSDataServicesEnableTiles);
			yield return new Guid(Permissions.WorkflowIMSDataServicesEzeSoftConnectionAdministration);
			yield return new Guid(Permissions.WorkflowIMSDataServicesInterfaceSupport);
			yield return new Guid(Permissions.WorkflowTradingAllocations);
			yield return new Guid(Permissions.WorkflowTradingBlotter);
			yield return new Guid(Permissions.WorkflowTradingEquityOrderAllocation);
			yield return new Guid(Permissions.WorkflowTradingEquityOrderEntry);
			yield return new Guid(Permissions.WorkflowTradingEquityOrderRouting);
			yield return new Guid(Permissions.WorkflowTradingOrderFillCapture);
			yield return new Guid(Permissions.WorkflowIMSReferenceDataRatesEditor);
			yield return new Guid(Permissions.DataLibraryAllocationSchemeCreate);
			yield return new Guid(Permissions.DataLibraryAllocationSchemeDelete);
			yield return new Guid(Permissions.DataLibraryAllocationSchemeRead);
			yield return new Guid(Permissions.DataLibraryAllocationSchemeUpdate);
			yield return new Guid(Permissions.DataLibraryCounterPartyRead);
			yield return new Guid(Permissions.DataLibraryCountryRead);
			yield return new Guid(Permissions.DataLibraryExchangeRead);
			yield return new Guid(Permissions.DataLibraryHolidayCalendarRead);
			yield return new Guid(Permissions.DataLibraryPortfolioRead);
			yield return new Guid(Permissions.DataLibraryPositionClosingSchemeRead);
			yield return new Guid(Permissions.DataLibraryPositionIndexRead);
			yield return new Guid(Permissions.DataLibraryPositionTagRead);
			yield return new Guid(Permissions.DataLibrarySecurityRead);
			yield return new Guid(Permissions.DataLibrarySecurityTemplateRead);
		}
	}
}
