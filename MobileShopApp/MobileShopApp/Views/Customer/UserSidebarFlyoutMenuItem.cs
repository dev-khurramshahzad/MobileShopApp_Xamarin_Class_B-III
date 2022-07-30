using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopApp.Views.Customer
{
    public class UserSidebarFlyoutMenuItem
    {
        public UserSidebarFlyoutMenuItem()
        {
            TargetType = typeof(UserSidebarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}