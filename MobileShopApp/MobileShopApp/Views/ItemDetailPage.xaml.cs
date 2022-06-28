using MobileShopApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobileShopApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}