using Plugin.Geolocator;
using MobileShopApp.Models;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using Firebase.Database.Query;

namespace MobileShopApp.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmOrder : ContentPage
    {
        public static Xamarin.Forms.Maps.Position Pos = new Xamarin.Forms.Maps.Position();
        public ConfirmOrder()
        {
            InitializeComponent();
            GetLocationMap();
        }

        public async void GetLocationMap()
        {
            try
            {
                await DisplayAlert("Message", "GPS and Internet will be used to access your current location. Please confirm that you have enabled the GPS and Internet.", "Ok");

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                Xamarin.Forms.Maps.Position TempPos = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                Pos = TempPos;

                MapView.Pins.Add(new Pin { Label = "Your Location", Position = Pos });

                MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));


            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong. This may be a problem with internet or application. Please ensure that you have a working internet connection and GPS enabled. \nError details : " + ex.Message, "Ok");
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnConfirm.IsVisible = true;
                btnCurrentLocation.IsVisible = true;
                btnProfileLocation.IsVisible = true;

                txtNewAddress.IsVisible = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }
        }

        private void btnFetch_Clicked_1(object sender, EventArgs e)
        {
            GetLocationMap();
        }

        private async void btnCurrentLocation_Clicked_1(object sender, EventArgs e)
        {
            
        }

        private async void btnProfileLocation_Clicked(object sender, EventArgs e)
        {
            try
            {

                // Creating New ID for Order ==================================

                int LastID, NewID = 1;
                var LastRecord = (await App.firebaseDatabase.Child("Order").OnceAsync<Order>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Order").OnceAsync<Order>()).Max(a => a.Object.OrderID);
                    NewID = ++LastID;
                }

                // Creating New Order Object for Order ==================================

                Order order = new Order()
                {
                    OrderID = NewID,
                    OrderDate = DateTime.Now.Date,
                    OrderTime = DateTime.Now.TimeOfDay,
                    Status = "Pending",
                    LocType = "Profile",
                    Address = App.LoggedInUser.Address,
                    UserID = App.LoggedInUser.UserId,
                };


                // Psting Order to firebase ==================================


                await App.firebaseDatabase.Child("Order").PostAsync(order);



                // Creating New ID for Order Details ==================================

                int LastID2, NewID2 = 1;
                var LastRecord2 = (await App.firebaseDatabase.Child("OrderDetail").OnceAsync<OrderDetail>()).FirstOrDefault();
                if (LastRecord2 != null)
                {
                    LastID2 = (await App.firebaseDatabase.Child("OrderDetail").OnceAsync<OrderDetail>()).Max(a => a.Object.OrderDetailID);
                    NewID2 = ++LastID2;
                }



                foreach (var item in App.Cart)
                {
                    item.OrderDetailID = NewID2;
                    item.OrderFID = NewID;

                    await App.firebaseDatabase.Child("OrderDetail").PostAsync(item);

                }


                App.Cart.Clear();

                Helpers.EmailHelper emailHelper = new Helpers.EmailHelper();
                
                emailHelper.EmailSender(App.LoggedInUser.Email, "Order Confirmed", "Order Confirmed");
               // emailHelper.MessageSender(App.LoggedInUser.Phone,  "Order Confirmed");



                App.Current.MainPage = new NavigationPage(new OrderConfirmation(NewID));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong. This may be a problem with internet or application. Please ensure that you have a working internet connection and GPS enabled. \nError details : " + ex.Message, "Ok");
            }
        }

        private async void btnNewAddress_Clicked_1(object sender, EventArgs e)
        {
           
        }

        private async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            
        }

    }

}