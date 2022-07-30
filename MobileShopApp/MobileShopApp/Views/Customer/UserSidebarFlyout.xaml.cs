using MobileShopApp.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSidebarFlyout : ContentPage
    {
        public ListView ListView;

        public UserSidebarFlyout()
        {
            InitializeComponent();

            BindingContext = new UserSidebarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class UserSidebarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserSidebarFlyoutMenuItem> MenuItems { get; set; }

            public UserSidebarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<UserSidebarFlyoutMenuItem>(new[]
               {
                    new UserSidebarFlyoutMenuItem { Id = 0, Icon="icon_feed.png", Title = "Home", TargetType = typeof(UserHome) },
                    new UserSidebarFlyoutMenuItem { Id = 1, Icon="icon_feed.png", Title = "View Categories", TargetType = typeof(CategoriesList) },
                    new UserSidebarFlyoutMenuItem { Id = 2, Icon="icon_feed.png", Title = "View Products", TargetType = typeof(UserHome) },
                    new UserSidebarFlyoutMenuItem { Id = 3, Icon="icon_feed.png", Title = "About", TargetType = typeof(UserHome) },
                    new UserSidebarFlyoutMenuItem { Id = 4, Icon="icon_feed.png", Title = "Contact", TargetType = typeof(UserHome) },
                    new UserSidebarFlyoutMenuItem { Id = 5, Icon="icon_feed.png", Title = "Rate Us", TargetType = typeof(UserHome) },
                    new UserSidebarFlyoutMenuItem { Id = 6, Icon="icon_feed.png", Title = "About Developer", TargetType = typeof(UserHome) },

                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}