using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileShopApp.Models
{
    public class Remember
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Detail { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }


    }
}
