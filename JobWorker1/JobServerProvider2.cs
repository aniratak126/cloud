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
    public class JobServerProvider2 : IForwardMessage
    {
        public void ForwardMessage(string str, string instanceId)
        {
            Trace.TraceInformation(String.Format("String {0} received from brother instance.", str));
            Console.WriteLine("String {0} received from brother instance.", str);
            string adresa = string.Format("net.tcp://{0}/Internal", RoleEnvironment.Roles["JobWorker2"].Instances[0].InstanceEndpoints["Internal"].IPEndpoint);

            var factory = new ChannelFactory<IForwardMessageToSecondRole>(new NetTcpBinding(), new EndpointAddress(adresa));
            var proxy = factory.CreateChannel();
            proxy.ForwardMessagdeToSecondRole(str, instanceId, RoleEnvironment.CurrentRoleInstance.Id);
        }
    }
}
