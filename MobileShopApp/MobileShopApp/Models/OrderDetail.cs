using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileShopApp.Models
{
  public  class OrderDetail
    {
        [PrimaryKey, AutoIncrement]
        public int OrderDetailID { get; set; }
        public int OrderFID { get; set; }
        public int ProductFID { get; set; }
        public int UnitQuantity { get; set; }
    }
}
