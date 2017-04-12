using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLToolkit;
using BLToolkit.DataAccess;
using DAL.Models;
using BLToolkit.Mapping;

namespace DAL.Accessors
{
    public abstract class NotificationListAccessor : DataAccessor<NotificationList>
    {
        [SprocName("sp_GetAllNotification")]
        public abstract List<NotificationList> GetAll();

        public static NotificationListAccessor CreateInstance()
        {
            return CreateInstance<NotificationListAccessor>();
        }
    }
}
