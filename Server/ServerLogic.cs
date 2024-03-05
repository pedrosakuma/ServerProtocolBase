using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProtocol.Server
{
    public class ServerLogic
    {
        public static void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}