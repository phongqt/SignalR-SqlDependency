using DAL.Helpers;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NotificationSignalR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterNotification();
        }

        private void RegisterNotification()
        {
            //Get the connection string from the Web.Config file. Make sure that the key exists and it is the connection string for the Notification Database and the NotificationList Table that we created

            string connectionString = ConfigurationManager.ConnectionStrings["NotificationSignalR"].ConnectionString;

            //We have selected the entire table as the command, so SQL Server executes this script and sees if there is a change in the result, raise the event
            string commandText = @"
                                    Select *                                     
                                    From
                                        dbo.NotificationList                                     
                                    ";

            //Start the SQL Dependency
            SqlDependency.Start(connectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    var sqlDependency = new SqlDependency(command);


                    sqlDependency.OnChange += new OnChangeEventHandler(sqlDependency_OnChange);

                    // NOTE: You have to execute the command, or the notification will never fire.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }
                }
            }
        }

        DateTime LastRun;

        private void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert)
            {
                //This is how signalrHub can be accessed outside the SignalR Hub Notification.cs file
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();


                NotificationListRepository objRepos = new NotificationListRepository(DbContext.Create());

                List<NotificationList> objList = objRepos.GetAll();

                LastRun = DateTime.Now.ToUniversalTime();


                foreach (var item in objList)
                {
                    //replace domain name with your own domain name
                    context.Clients.User("<DomainName>" + item.UserID).addLatestNotification(item);
                }

            }
            //Call the RegisterNotification method again
            RegisterNotification();
        }
    }
}
