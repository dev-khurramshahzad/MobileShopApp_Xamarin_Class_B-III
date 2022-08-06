
using MobileShopApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPageControls : ContentPage
    {

        public CartPageControls()
        {
            InitializeComponent();
            try
            {
                UpdateCartAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Somthing went wrong. This may be a problem with internet or application. Please ensure that you have a working internet connection and GPS enabled. \nError details : " + ex.Message, "Ok");
            }


        }

        async void UpdateCartAsync()
        {


            List<imageCell_VM> CartItems = new List<imageCell_VM>();
            double? Amount = 0;
            foreach (var item in App.Cart)
            {
                var prod = (await App.firebaseDatabase.Child("Items").OnceAsync<Models.Items>()).FirstOrDefault(x => x.Object.ItemID == item.ProductFID);

                double? total = prod.Object.SPrice * (item.UnitQuantity);
                Amount += total;

                CartItems.Add(new imageCell_VM
                {
                    ID = prod.Object.ItemID,
                    image = prod.Object.ItemImage,
                    Name = prod.Object.ItemName + "  - Rating: " + prod.Object.Rating,
                    Detail = "Rs. " + prod.Object.SPrice + " X  " + item.UnitQuantity + " = Total Rs. " + total.ToString()
                });
            }


            App.Total = Amount;

            lblTotal.Text = Amount.ToString();
            DataList.ItemsSource = CartItems;

        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (App.Cart.Count < 1)
            {
                await DisplayAlert("Message", "Cart page is empty. Please add at least one item in cart", "Ok");
                return;
            }

            var res = await DisplayActionSheet("Select payement method", "Close", "", "Cash on delivery", "Online payment");
            if (res == "Cash on delivery")
            {
                await Navigation.PushAsync(new ConfirmOrder());
            }

            if (res == "Online payment")
            {
                //await Navigation.PushAsync(new Payment());
            }

            
        }


        private async void btnRemove_Clicked(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = sender as ImageButton;
                var item = btn.CommandParameter as imageCell_VM;

                for (int i = 0; i < App.Cart.Count; i++)
                {
                    if (App.Cart[i].ProductFID == item.ID)
                    {
                        var res = await DisplayAlert("Question", "Are you sure you want to remove " + item.Name + "?", "Yes", "No");
                        if (res)
                        {
                            App.Cart.RemoveAt(i);
                        }
                    }
                }

                UpdateCartAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong. This may be a problem with internet or application. Please ensure that you have a working internet connection and GPS enabled. \nError details : " + ex.Message, "Ok");
            }

        }
    }
}