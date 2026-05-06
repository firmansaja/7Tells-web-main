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
    public class TestHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("getKanbans")]
        public static void GetKanbans()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.updateData();
        }
    }
}
