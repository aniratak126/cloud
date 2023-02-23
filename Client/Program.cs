using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Enter message: ");
                string str = Console.ReadLine();

                string adresa = "net.tcp://localhost:10100/Input";
                var factory = new ChannelFactory<IJob>(new NetTcpBinding(), new EndpointAddress(adresa));
                var proxy = factory.CreateChannel();
                proxy.SendMessage(str);
            }
        }
    }
}
