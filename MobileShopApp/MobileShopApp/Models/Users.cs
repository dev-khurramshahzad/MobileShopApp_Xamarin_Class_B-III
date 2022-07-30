using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileShopApp.Models
{
    public class Users
    {
        [PrimaryKey,AutoIncrement]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
