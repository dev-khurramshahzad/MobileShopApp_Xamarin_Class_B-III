using Firebase.Database.Query;
using MobileShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersList : ContentPage
    {
        public UsersList()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).Select(x => new Users
            {
                UserId = x.Object.UserId,
                Name = x.Object.Name,
                Email = x.Object.Email,
                Password = x.Object.Password,
                Phone = x.Object.Phone

            }).ToList();
        }




        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Users;

            var item = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).FirstOrDefault(a => a.Object.UserId == selected.UserId);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "Favorite", "Archieved");

            if (choice == "View")
            {
                await DisplayAlert("Details", "" +
                    "\nUser ID : " + item.Object.UserId +
                    "\nName : " + item.Object.Name +
                    "\nEmail : " + item.Object.Email +
                    "\nPhone : " + item.Object.Phone +
                    "\nPassword : *******" +
                    "", "OK");
            }
            if (choice == "Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you sure you want to delete " + item.Object.Name, "Yes", "No");
                if (q)
                {
                    await App.firebaseDatabase.Child("Users").Child(item.Key).DeleteAsync();
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