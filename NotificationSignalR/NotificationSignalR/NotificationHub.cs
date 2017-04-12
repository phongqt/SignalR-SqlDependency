using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DAL.Repositories;
using System.Security.Principal;
using DAL.Interfaces;

namespace NotificationSignalR
{

    public class NotificationHub : Hub
    {
        private INotification objRepository = null;
        public NotificationHub(INotification objRepository)
        {
            this.objRepository = objRepository;
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public void SendNotification(string message, string user)
        {
            objRepository.AddNotification(message, user);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            Console.Write(Context.User.Identity.GetLogin().ToString());
            Clients.User(Context.User.Identity.Name).refreshNotification(objRepository.GetAll());

            return base.OnConnected();
        }


    }

    public static class Extensions
    {
        public static string GetDomain(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(0, stop) : string.Empty;
        }

        public static string GetLogin(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : string.Empty;
        }
    }
}