using DAL.Helpers;
using DAL.Repositories;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NotificationSignalR.Startup))]

namespace NotificationSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
        typeof(NotificationHub),
        () => new NotificationHub(new NotificationListRepository(DbContext.Create())));
            app.MapSignalR();
        }
    }
}
