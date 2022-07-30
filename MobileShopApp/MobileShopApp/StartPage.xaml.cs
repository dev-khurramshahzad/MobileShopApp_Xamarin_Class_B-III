using MobileShopApp.LoginSystem;
using MobileShopApp.Views.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

       

        private void btnGetStarted_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserSidebar();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginSystem.Login());
        }
    }
}