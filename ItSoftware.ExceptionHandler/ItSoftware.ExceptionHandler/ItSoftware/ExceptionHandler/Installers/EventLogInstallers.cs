using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.ComponentModel;

namespace ItSoftware.ExceptionHandler.Installers
{
    [RunInstaller(true)]
    public class MyEventLogInstaller : Installer
    {
        private EventLogInstaller compuFlowRetrivalEventLogInstaller;
        private EventLogInstaller compuFlowGeneratorEventLogInstaller;
        private EventLogInstaller compuFlowPublisherEventLogInstaller;
        private EventLogInstaller compuFlowEventsEventLogInstaller;
        private EventLogInstaller compuFlowGatewayEventLogInstaller;
        private EventLogInstaller compuFlowControlCenterInstaller;
        private EventLogInstaller testEventLogInstaller;


        public MyEventLogInstaller()
        {
            // Create instances of an EventLogInstaller.
            compuFlowRetrivalEventLogInstaller = new EventLogInstaller();
            compuFlowGeneratorEventLogInstaller = new EventLogInstaller();
            compuFlowPublisherEventLogInstaller = new EventLogInstaller();
            compuFlowEventsEventLogInstaller = new EventLogInstaller();
            compuFlowGatewayEventLogInstaller = new EventLogInstaller();
            compuFlowControlCenterInstaller = new EventLogInstaller();
            testEventLogInstaller = new EventLogInstaller();
           
            // Set the source name of the event log.
            compuFlowRetrivalEventLogInstaller.Source = "CompuFlow - Retrival";
            compuFlowGeneratorEventLogInstaller.Source = "CompuFlow - Generator";
            compuFlowPublisherEventLogInstaller.Source = "CompuFlow - Publisher";
            compuFlowEventsEventLogInstaller.Source = "CompuFlow - Events";
            compuFlowGatewayEventLogInstaller.Source = "CompuFlow - Gateway";
            compuFlowControlCenterInstaller.Source = "CompuFlow - Control Center";
            testEventLogInstaller.Source = "Test";


            // Set the event log that the source writes entries to.
            compuFlowRetrivalEventLogInstaller.Log = "ItSoftware";
            compuFlowGeneratorEventLogInstaller.Log = "ItSoftware";
            compuFlowPublisherEventLogInstaller.Log = "ItSoftware";
            compuFlowEventsEventLogInstaller.Log = "ItSoftware";
            compuFlowGatewayEventLogInstaller.Log = "ItSoftware";
            compuFlowControlCenterInstaller.Log = "ItSoftware";
            testEventLogInstaller.Log = "ItSoftware";

            // Add myEventLogInstaller to the Installer collection.
            Installers.Add(compuFlowRetrivalEventLogInstaller);
            Installers.Add(compuFlowGeneratorEventLogInstaller);
            Installers.Add(compuFlowPublisherEventLogInstaller);
            Installers.Add(compuFlowEventsEventLogInstaller);
            Installers.Add(compuFlowGatewayEventLogInstaller);
            Installers.Add(compuFlowControlCenterInstaller);
            Installers.Add(testEventLogInstaller);
        }
    }
}