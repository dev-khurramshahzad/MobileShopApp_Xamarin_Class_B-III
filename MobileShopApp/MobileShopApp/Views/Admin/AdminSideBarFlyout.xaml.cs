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

namespace MobileShopApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public AdminSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AdminSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminSideBarFlyoutMenuItem> MenuItems { get; set; }

            public AdminSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminSideBarFlyoutMenuItem>(new[]
                {
                    new AdminSideBarFlyoutMenuItem { Id = 0, Icon="icon_feed.png", Title = "Users List", TargetType = typeof(LoginSystem.UsersList) },
                    new AdminSideBarFlyoutMenuItem { Id = 1, Icon="icon_feed.png", Title = "Add Category", TargetType = typeof(Add_Category) },
                    new AdminSideBarFlyoutMenuItem { Id = 2,Icon="icon_feed.png", Title = "Manage Category", TargetType = typeof(Manage_Category) },
                    new AdminSideBarFlyoutMenuItem { Id = 3, Icon="icon_feed.png", Title = "Add Product", TargetType = typeof(Add_Product) },
                    new AdminSideBarFlyoutMenuItem { Id = 3, Icon="icon_feed.png", Title = "Manage Products", TargetType = typeof(Manage_Products) },

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