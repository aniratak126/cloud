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
    public class JobServer2
    {
        ServiceHost serviceHost;
        private string externalEndpointName = "Internal";
        public JobServer2()
        {
            RoleInstanceEndpoint instanceEndpoint =
                RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = string.Format("net.tcp://{0}/{1}", instanceEndpoint.IPEndpoint, externalEndpointName);
            serviceHost = new ServiceHost(typeof(JobServerProvider2));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IForwardMessage), binding, endpoint);
                
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type opened succesffully at {1}.", externalEndpointName, DateTime.Now));

            } catch(Exception e)
            {
                Trace.TraceInformation(string.Format("Host open error for {0} enpoint type. Err mess is: ", externalEndpointName, e.Message));
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(string.Format("Host for {0} endpoint type closed succesffully at {1}.", externalEndpointName, DateTime.Now));

            }
            catch (Exception e)
            {
                Trace.TraceInformation(string.Format("Host close error for {0} enpoint type. Err mess is: ", externalEndpointName, e.Message));
            }
        }
    }
}
