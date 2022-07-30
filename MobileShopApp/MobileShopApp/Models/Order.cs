using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileShopApp.Models
{
   public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public TimeSpan OrderTime { get; set; }


        public string LocType { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Address { get; set; }
        public string Details { get; set; }

        public string Status { get; set; }

    }
}
