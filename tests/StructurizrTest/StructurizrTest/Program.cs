using System.Linq;
using Structurizr.Client;
using Structurizr.Model;
using Structurizr.View;

namespace Structurizr.Examples
{
    /// <summary>
    ///     This is a simple example of how to create a software architecture model using Structurizr. The model
    ///     represents a sample solution for the "Financial Risk System" architecture kata, included in
    ///     "The Art of Visualising Software Architecture" book (available FREE from Leanpub).
    ///     The live version of the diagrams can be found at https://structurizr.com/public/9481
    /// </summary>
    internal class FinancialRiskSystem
    {
        private const string AlertTag = "Alert";

        private static void TestComponentFinder()
        {
            //var finder = new ComponentFinder();
        }

        private static void Main(string[] args)
        {
            var workspace = new Workspace("Financial Risk System",
                "A simple example C4 model based upon the financial risk system architecture kata, created using Structurizr for .NET");
            var model = workspace.Model;

            // create the basic model
            var financialRiskSystem = model.AddSoftwareSystem(Location.Internal, "Financial Risk System",
                "Calculates the bank's exposure to risk for product X");

            var businessUser = model.AddPerson(Location.Internal, "Business User", "A regular business user");
            businessUser.Uses(financialRiskSystem, "Views reports using");

            var configurationUser = model.AddPerson(Location.Internal, "Configuration User",
                "A regular business user who can also configure the parameters used in the risk calculations");
            configurationUser.Uses(financialRiskSystem, "Configures parameters using");

            var tradeDataSystem = model.AddSoftwareSystem(Location.Internal, "Trade Data System",
                "The system of record for trades of type X");
            financialRiskSystem.Uses(tradeDataSystem, "Gets trade data from");

            var referenceDataSystem = model.AddSoftwareSystem(Location.Internal, "Reference Data System",
                "Manages reference data for all counterparties the bank interacts with");
            financialRiskSystem.Uses(referenceDataSystem, "Gets counterparty data from");

            var emailSystem = model.AddSoftwareSystem(Location.Internal, "E-mail system", "Microsoft Exchange");
            financialRiskSystem.Uses(emailSystem, "Sends a notification that a report is ready to");
            emailSystem.Delivers(businessUser, "Sends a notification that a report is ready to", "E-mail message",
                InteractionStyle.Asynchronous);

            var centralMonitoringService = model.AddSoftwareSystem(Location.Internal, "Central Monitoring Service",
                "The bank-wide monitoring and alerting dashboard");
            financialRiskSystem.Uses(centralMonitoringService, "Sends critical failure alerts to", "SNMP",
                InteractionStyle.Asynchronous).AddTags(AlertTag);

            var activeDirectory = model.AddSoftwareSystem(Location.Internal, "Active Directory",
                "Manages users and security roles across the bank");
            financialRiskSystem.Uses(activeDirectory, "Uses for authentication and authorisation");

            var webApplication = financialRiskSystem.AddContainer("Web Application",
                "Allows users to view reports and modify risk calculation parameters", "ASP.NET MVC");
            businessUser.Uses(webApplication, "Views reports using");
            configurationUser.Uses(webApplication, "Modifies risk calculation parameters using");
            webApplication.Uses(activeDirectory, "Uses for authentication and authorisation");

            var batchProcess = financialRiskSystem.AddContainer("Batch Process", "Calculates the risk",
                "Windows Service");
            batchProcess.Uses(emailSystem, "Sends a notification that a report is ready to");
            batchProcess.Uses(tradeDataSystem, "Gets trade data from");
            batchProcess.Uses(referenceDataSystem, "Gets counterparty data from");
            batchProcess.Uses(centralMonitoringService, "Sends critical failure alerts to", "SNMP",
                InteractionStyle.Asynchronous).AddTags(AlertTag);

            var fileSystem = financialRiskSystem.AddContainer("File System", "Stores risk reports", "Network File Share");
            webApplication.Uses(fileSystem, "Consumes risk reports from");
            batchProcess.Uses(fileSystem, "Publishes risk reports to");

            var scheduler = batchProcess.AddComponent("Scheduler",
                "Starts the risk calculation process at 5pm New York time", "Quartz.NET");
            var orchestrator = batchProcess.AddComponent("Orchestrator", "Orchestrates the risk calculation process",
                "C#");
            var tradeDataImporter = batchProcess.AddComponent("Trade data importer",
                "Imports data from the Trade Data System", "C#");
            var referenceDataImporter = batchProcess.AddComponent("Reference data importer",
                "Imports data from the Reference Data System", "C#");
            var riskCalculator = batchProcess.AddComponent("Risk calculator", "Calculates risk", "C#");
            var reportGenerator = batchProcess.AddComponent("Report generator",
                "Generates a Microsoft Excel compatible risk report", "C# and Microsoft.Office.Interop.Excel");
            var reportPublisher = batchProcess.AddComponent("Report distributor",
                "Publishes the report to the web application", "C#");
            var emailComponent = batchProcess.AddComponent("E-mail component", "Sends e-mails", "C#");
            var reportChecker = batchProcess.AddComponent("Report checker",
                "Checks that the report has been generated by 9am singapore time", "C#");
            var alertComponent = batchProcess.AddComponent("Alert component", "Sends SNMP alerts",
                "C# and #SNMP Library");

            scheduler.Uses(orchestrator, "Starts");
            scheduler.Uses(reportChecker, "Starts");
            orchestrator.Uses(tradeDataImporter, "Imports data using");
            tradeDataImporter.Uses(tradeDataSystem, "Imports data from");
            orchestrator.Uses(referenceDataImporter, "Imports data using");
            referenceDataImporter.Uses(referenceDataSystem, "Imports data from");
            orchestrator.Uses(riskCalculator, "Calculates the risk using");
            orchestrator.Uses(reportGenerator, "Generates the risk report using");
            orchestrator.Uses(reportPublisher, "Publishes the risk report using");
            reportPublisher.Uses(fileSystem, "Publishes the risk report to");
            orchestrator.Uses(emailComponent, "Sends e-mail using");
            emailComponent.Uses(emailSystem, "Sends a notification that a report is ready to");
            reportChecker.Uses(alertComponent, "Sends alerts using");
            alertComponent.Uses(centralMonitoringService, "Sends alerts using", "SNMP", InteractionStyle.Asynchronous)
                .AddTags(AlertTag);

            // create some views
            var viewSet = workspace.Views;
            var contextView = viewSet.CreateContextView(financialRiskSystem);
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            var containerView = viewSet.CreateContainerView(financialRiskSystem);
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            var componentViewForBatchProcess = viewSet.CreateComponentView(batchProcess);
            contextView.PaperSize = PaperSize.A3_Landscape;
            componentViewForBatchProcess.AddAllElements();
            componentViewForBatchProcess.Remove(configurationUser);
            componentViewForBatchProcess.Remove(webApplication);
            componentViewForBatchProcess.Remove(activeDirectory);

            // tag and style some elements
            var styles = viewSet.Configuration.Styles;
            financialRiskSystem.AddTags("Risk System");

            styles.Add(new ElementStyle(Tags.Element) { Color = "#ffffff", FontSize = 34 });
            styles.Add(new ElementStyle("Risk System") { Background = "#8a458a" });
            styles.Add(new ElementStyle(Tags.SoftwareSystem)
            {
                Width = 650,
                Height = 400,
                Background = "#510d51",
                //Shape = Shape.Box
            });
            styles.Add(new ElementStyle(Tags.Person)
            {
                Width = 550,
                Background = "#62256e",
                //Shape = Shape.Person
            });
            styles.Add(new ElementStyle(Tags.Container)
            {
                Width = 650,
                Height = 400,
                Background = "#a46ba4",
                //Shape = Shape.Box
            });
            styles.Add(new ElementStyle(Tags.Component)
            {
                Width = 550,
                Background = "#c9a1c9",
                //Shape = Shape.Box
            });

            styles.Add(new RelationshipStyle(Tags.Relationship)
            {
                Thickness = 4,
                Dashed = false,
                FontSize = 32,
                Width = 400
            });
            styles.Add(new RelationshipStyle(Tags.Synchronous) { Dashed = false });
            styles.Add(new RelationshipStyle(Tags.Asynchronous) { Dashed = true });
            styles.Add(new RelationshipStyle(AlertTag) { Color = "#ff0000" });

            businessUser.Relationships.ToList().ForEach(r => r.AddTags("HTTPS"));

            // and upload the model to structurizr.com
            var structurizrClient = new StructurizrClient("4fd23f21-0139-4fb3-b832-22a1fc8b8d83", "d439618a-336d-4c37-b6fe-20c0846ffbd9");
            //structurizrClient.PutWorkspace(9861L, workspace);
            structurizrClient.MergeWorkspace(9861L, workspace);
        }
    }
}