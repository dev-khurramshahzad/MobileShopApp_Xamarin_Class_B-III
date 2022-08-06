using Firebase.Database.Query;
using MobileShopApp.Models;
using MobileShopApp.Views.Admin;
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
    public partial class Manage_Products : ContentPage
    {
        public Manage_Products()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadData();
                LoadingInd.IsRunning = false;

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please try again later.\nError: " + ex.Message, "OK");
            }


        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Items").OnceAsync<Items>()).Select(x => new Items
            {
                ItemID = x.Object.ItemID,
                CatFID = x.Object.CatFID,
                ItemImage = x.Object.ItemImage,
                ItemDetail = x.Object.ItemDetail,
                ItemName = x.Object.ItemName,
                ItemStatus = x.Object.ItemStatus,
                PPrice = x.Object.PPrice,
                Quantity = x.Object.Quantity,
                Rating = x.Object.Rating,
                SPrice = x.Object.SPrice,


            }).ToList();
        }




        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Items;

            var item = (await App.firebaseDatabase.Child("Items").OnceAsync<Items>()).FirstOrDefault(a => a.Object.ItemID == selected.ItemID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "Favorite", "Archieved");

            if (choice == "View")
            {
                //await Navigation.PushAsync(new CategoryDetails(selected));
            }
            if (choice == "Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you sure you want to delete " + item.Object.ItemName, "Yes", "No");
                if (q)
                {
                    await App.firebaseDatabase.Child("Items").Child(item.Key).DeleteAsync();
                    LoadData();

                    await DisplayAlert("Confirmation", item.Object.ItemName + " Deleted Permanently", "OK");
                }

            }
            if (choice == "Edit")
            {
               // await Navigation.PushAsync(new Edit_Category(selected));

            }


        }
    }
}