using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker1
{
    public class JobServer
    {
        ServiceHost serviceHost;
        private string externalEndpointName = "Input";

        public JobServer()
        {
            RoleInstanceEndpoint inputEndPoint =
                RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = string.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, externalEndpointName);
            serviceHost = new ServiceHost(typeof(JobServerProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IJob), binding, endpoint);

        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type opened successfully at {1}.", externalEndpointName, DateTime.Now));

            } catch(Exception e)
            {
                Trace.TraceInformation(string.Format("Host open error for {0} endpoint type. Error Mess is: {1}", externalEndpointName, e.Message));
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type closed successfully at {1}.", externalEndpointName, DateTime.Now));

            }
            catch (Exception e)
            {
                Trace.TraceInformation(string.Format("Host close error for {0} endpoint type. Error Mess is: {1}", externalEndpointName, e.Message));
            }
        }
    }
}
