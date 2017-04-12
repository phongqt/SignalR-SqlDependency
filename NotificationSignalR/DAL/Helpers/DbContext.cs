using System.Data.Linq;
using System.Configuration;
using DAL.Models;

namespace DAL.Helpers
{
    public class DbContext: DataContext
    {
        static string connectionStr = ConfigurationManager.ConnectionStrings["NotificationSignalR"].ConnectionString;

        public DbContext()
            : base(connectionStr)
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }

        public Table<NotificationList> Notifications;
    }
}
