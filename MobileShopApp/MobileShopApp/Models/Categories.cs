using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileShopApp.Models
{
    public class Categories
    {
        [PrimaryKey,AutoIncrement]
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
    }
}
