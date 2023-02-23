using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace JobWorker2
{
    public class JobServerProvideer : IForwardMessageToSecondRole
    {
        List<Entity> entities = new List<Entity>();
        public void ForwardMessagdeToSecondRole(string str, string firstInstanceId, string secondInstanceId)
        {
            Trace.TraceInformation(string.Format("String {0} received from JobWokrker1.", str));
            Console.WriteLine("String {0} received from JobWokrker1.", str);
            entities.Add(new Entity(firstInstanceId, secondInstanceId, str));
        }
    }
}
