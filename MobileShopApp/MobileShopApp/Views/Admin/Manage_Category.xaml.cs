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
    public partial class Manage_Category : ContentPage
    {
        public Manage_Category()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).Select(x => new Categories
            {
                CatID = x.Object.CatID,
                Name = x.Object.Name,
                Details = x.Object.Details,
                Image = x.Object.Image,

            }).ToList();
        }




        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Categories;

            var item = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).FirstOrDefault(a => a.Object.CatID == selected.CatID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "Favorite", "Archieved");

            if (choice == "View")
            {
                

                await Navigation.PushAsync(new CategoryDetails(selected));

            }
            if (choice == "Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you sure you want to delete " + item.Object.Name, "Yes", "No");
                if (q)
                {
                    await App.firebaseDatabase.Child("Categories").Child(item.Key).DeleteAsync();
                    LoadData();

                    await DisplayAlert("Confirmation", item.Object.Name + " Deleted Permanently", "OK");
                }

            }
            if (choice == "Edit")
            {

            }


        }
    }
}