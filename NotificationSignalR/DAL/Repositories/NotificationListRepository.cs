using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using DAL.Models;
using BLToolkit.Data.Sql;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class NotificationListRepository : INotification
    {
        private DbContext dbContext;

        public NotificationListRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<NotificationList> GetAll()
        {
            var list = this.dbContext.ExecuteQuery<NotificationList>("exec [sp_GetAllNotification]").ToList();
            return list;
        }

        public void AddNotification(string message, string userName)
        {
            NotificationList objCourse = new NotificationList();
            objCourse.Text = message;
            objCourse.UserID = userName;
            objCourse.CreatedDate = DateTime.Now;
            dbContext.Notifications.InsertOnSubmit(objCourse);
            dbContext.SubmitChanges();
        }
    }
}
