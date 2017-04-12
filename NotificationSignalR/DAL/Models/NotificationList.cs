using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table(Name = "NotificationList")]
    public class NotificationList
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string Text { get; set; }
        [Column]
        public string UserID { get; set; }
        [Column]
        public DateTime CreatedDate { get; set; }
    }
}
