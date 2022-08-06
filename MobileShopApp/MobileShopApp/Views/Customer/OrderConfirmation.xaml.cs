using MobileShopApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderConfirmation : ContentPage
    {
        public static int id;
        public OrderConfirmation(int OrderID)
        {
            InitializeComponent();
            id = OrderID;
            LoadData(OrderID);
           
        }

        private async void LoadData(int Id)
        {
            var order = (await App.firebaseDatabase.Child("Order").OnceAsync<Order>()).FirstOrDefault(x=>x.Object.OrderID == Id);

            lblCustomer.Text = App.LoggedInUser.Name;
            lblDate.Text = order.Object.OrderDate.ToShortDateString();
            lblID.Text = "MS202200-"+order.Object.OrderID.ToString();
        }
    }
}