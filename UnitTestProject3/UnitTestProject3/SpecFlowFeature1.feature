Feature: TradingWorkflowPermission
	In order to make users perform Trading workflow
	As a provisioning user
	I require the ability to turn on the trading workflow for a role

@Functional
Scenario: Trading Workflow Permissions Exist in Role Template 
	Given The Trading External Roles exist
    Then I should see the permissions
	| Workflow                | Id                                   |
	| Blotter                 | 59C0FE11-DECC-4A0A-90BF-3433B4DB0F62 |
	| Equity Order Entry      | 2C6D7D24-DAF8-4349-BCBD-5C5C28AE44DB |
	| Equity Order Routing    | 91E2FC09-463F-48C5-910D-6D01E5ABAE8F |
	| Equity Order Allocation | A6119EBC-3A7F-4157-A474-FD4486B12971 |
