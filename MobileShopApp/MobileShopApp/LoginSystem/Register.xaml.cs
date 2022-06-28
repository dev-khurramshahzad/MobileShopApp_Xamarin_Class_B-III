using Firebase.Database.Query;
using MobileShopApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {


                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    await DisplayAlert("Error", "Please fill all required fields and try again", "OK");
                    return;
                }

                if (txtCPassword.Text != txtPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }

                //App.db.CreateTable<Users>();
                //var check = App.db.Table<Users>().FirstOrDefault(x => x.Email == txtEmail.Text);

                //if (check != null)
                //{
                //    await DisplayAlert("Error", "This email is already registerd", "OK");
                //    return;
                //}

                LoadingInd.IsRunning = true;


                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).Max(a => a.Object.UserId);
                    NewID = ++LastID;
                }
                

                Users u = new Users()
                {
                    UserId = NewID,
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Phone = txtPhone.Text,

                };




                await App.firebaseDatabase.Child("Users").PostAsync(u);

                LoadingInd.IsRunning = false;


                await DisplayAlert("Success", "Account Registered", "OK");
                await Navigation.PushAsync(new Login());
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please try again later.\nError: " + ex.Message, "OK");

            }


        }
    }
}