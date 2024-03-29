﻿using MobileShopApp.Models;
using MobileShopApp.Views.Admin;
using MobileShopApp.Views.Customer;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                await DisplayAlert("Error", "Please fill all required fields and try again", "OK");
                return;
            }

            try
            {
                LoadingInd.IsRunning = true;

                var check = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).FirstOrDefault(x => x.Object.Email == txtEmail.Text && x.Object.Password == txtPass.Text && x.Object.UserType == "Admin");



                if (check == null)
                {
                    LoadingInd.IsRunning = false;
                    await DisplayAlert("Error", "Email or password incorrect", "OK");
                    return;
                }
                else
                {
                    App.Current.MainPage = new AdminSideBar();
                }

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please try again later.\nError: " + ex.Message, "OK");

            }



        }

       
    }
}