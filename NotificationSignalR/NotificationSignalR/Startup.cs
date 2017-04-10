using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NotificationSignalR.Startup))]

namespace NotificationSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
