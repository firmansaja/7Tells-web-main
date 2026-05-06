using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Helpers
{
    [HubName("fifoHub")]
    public class FifoHub : Hub
    {
    

        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("getFifo")]
        public static void GetFifo()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

        [HubMethodName("getLPH")]
        public static void GetLPH()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

        [HubMethodName("getTrolly")]
        public static void GetTrolly()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

        [HubMethodName("getTrollyDepo")]
        public static void GetTrollyDepo()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

        [HubMethodName("getLineStore")]
        public static void GetLineStore()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

        [HubMethodName("getTrollyStokDepo")]
        public static void GetTrollyStokDepo()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FifoHub>();
            context.Clients.All.updateData();
        }

    }
}
