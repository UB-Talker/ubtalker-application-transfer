using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBTalker.Services
{
    class CallLightService : ICallLightService
    {
        public void ActivateCallLight()
        {
            System.Console.WriteLine("Call light will eventually turn on here");
        }
    }
}
