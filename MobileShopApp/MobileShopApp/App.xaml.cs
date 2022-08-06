using MobileShopApp.Services;
using MobileShopApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileShopApp.LoginSystem;
using System.IO;
using SQLite;
using Firebase.Storage;
using Firebase.Database;
using MobileShopApp.Views.Admin;
using System.Collections.Generic;
using MobileShopApp.Models;

namespace MobileShopApp
{
    public partial class App : Application
    {
        //public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMobileShopApp.db3");
        //public static SQLiteConnection db = new SQLiteConnection(dbPath);

        //Firebase Connections  ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("mobileshopapp-998a1.appspot.com");
        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://mobileshopapp-998a1-default-rtdb.firebaseio.com/");

        public static List<OrderDetail> Cart = new List<OrderDetail>();
        public static double? Total = null;
        public static Users LoggedInUser = null;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new SplashScreen();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
