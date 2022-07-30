using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileShopApp.Models
{
   public class Items
    { 
        [PrimaryKey,AutoIncrement]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int  CatFID { get; set; }
        public int Quantity { get; set; }
        public string Rating { get; set; }
        public float SPrice { get; set; }
        public float PPrice { get; set; }
        public string ItemImage { get; set; }
        public string ItemDetail { get; set; }
        public string ItemStatus { get; set; }
    }
}
