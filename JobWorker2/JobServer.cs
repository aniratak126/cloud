using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.Diagnostics;

namespace JobWorker2
{
    public class JobServer
    {
        ServiceHost serviceHost;
        private string externalEndpointName = "Internal";
        public JobServer()
        {
            RoleInstanceEndpoint instanceEndpoint =
                RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = string.Format("net.tcp://{0}/{1}", instanceEndpoint.IPEndpoint, externalEndpointName);
            serviceHost = new ServiceHost(typeof(JobServerProvideer));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IForwardMessageToSecondRole), binding, endpoint);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type succesffully opened at {1}.", externalEndpointName, DateTime.Now));
            } catch(Exception e)
            {
                Trace.TraceInformation(string.Format("Host open error for {0} endpont type, Err mess is: {0}.", externalEndpointName, e.Message));
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type succesffully closed at {1}.", externalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation(string.Format("Host close error for {0} endpont type, Err mess is: {0}.", externalEndpointName, e.Message));
            }
        }
    }
}
