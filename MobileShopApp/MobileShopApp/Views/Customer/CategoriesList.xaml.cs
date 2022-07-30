using MobileShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesList : ContentPage
    {
        public CategoriesList()
        {
            InitializeComponent();
            LoadData();
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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }



      
}