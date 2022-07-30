using MobileShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile(Users u)
        {
            InitializeComponent();

            lblName.Text = u.Name;
            lblPhone.Text = u.Phone;
            lblEmail.Text = u.Email;
            lblID.Text = u.UserId.ToString();
            lblName.Text = u.Name;

            
        }
    }
}