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

namespace MobileShopApp
{
    public partial class App : Application
    {
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMobileShopApp.db3");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);

        //Firebase Connections  ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("gs://mobileshopapp-998a1.appspot.com");
        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://mobileshopapp-998a1-default-rtdb.firebaseio.com/");


        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new StartPage();
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
