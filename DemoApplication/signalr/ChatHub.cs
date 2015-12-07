using DemoApplication.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private readonly PackagesList packagesList;

        public ChatHub(PackagesList list)
        {
            this.packagesList = list;
            this.packagesList.AddPackage += PackagesList_AddPackage;
        }

        private void PackagesList_AddPackage(PackagesList self, string query, NuGet.IPackage package)
        {
            this.Send("automation", package.Id);
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}