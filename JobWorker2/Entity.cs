using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker2
{
    public class Entity
    {
        public string FirstInstanceId { get; set; }
        public string SecondInstanceId { get; set; }
        public string Message { get; set; }

        public Entity(string first, string second, string mess)
        {
            FirstInstanceId = first;
            SecondInstanceId = second;
            Message = mess;
        }
    }
}
