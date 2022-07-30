using MobileShopApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileShopApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetails : ContentPage
    {
        public CategoryDetails(Categories c)
        {
            InitializeComponent();

            CatDetails.Text = c.Details;
            CatID.Text = c.CatID.ToString();
            CatName.Text = c.Name;
            CatImage.Source = c.Image;
        }
    }
}