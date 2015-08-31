using System;
using TechTalk.SpecFlow;

namespace UnitTestProject3
{
    [Binding]
    public class TradingWorkflowPermissionSteps
    {
        [Given(@"The Trading External Roles exist")]
        public void GivenTheTradingExternalRolesExist()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should see the permissions")]
        public void ThenIShouldSeeThePermissions(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
