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
    public class JobServerProvider : IJob
    {
        public void SendMessage(string str)
        {
            Trace.TraceInformation(string.Format("String {0} received from client.", str));
            Console.WriteLine("String {0} received from client.", str);
            int nextIndex = GetIndex(RoleEnvironment.CurrentRoleInstance.Id) + 1;
            nextIndex = nextIndex == RoleEnvironment.Roles["JobWorker1"].Instances.Count ? 0 : nextIndex;
            Trace.TraceInformation(String.Format("Instance id [nextIndex]: {0}", nextIndex));

            string adresa = "";
            foreach(var item in RoleEnvironment.Roles["JobWorker1"].Instances)
            {
                if(nextIndex == GetIndex(item.Id))
                {
                    adresa = string.Format("net.tcp://{0}/Internal",
                        item.InstanceEndpoints["Internal"].IPEndpoint);
                    break;
                }
            }

            var factory = new ChannelFactory<IForwardMessage>(new NetTcpBinding(), new EndpointAddress(adresa));
            var proxy = factory.CreateChannel();
            proxy.ForwardMessage(str, RoleEnvironment.CurrentRoleInstance.Id);
        }

        private int GetIndex(string instanceId)
        {
            int instanceIndex = 0;
            if (!int.TryParse(instanceId.Substring(instanceId.LastIndexOf(".") + 1), out instanceIndex))
            {
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf("_") + 1), out instanceIndex);
            }
            return instanceIndex;
        }
    }
}
