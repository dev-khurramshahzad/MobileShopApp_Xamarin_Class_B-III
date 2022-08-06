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
    public partial class Items : ContentPage
    {
        public Items(int id)
        {
            InitializeComponent();
            try
            {
                LoadData(id); 
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", " Something went wrong...\n\n Errors details.." + ex.Message, "Ok");


            }

        }

        async void LoadData(int id)
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Items").OnceAsync<Models.Items>()).Select(x => new Models.Items
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


            }).Where(x=>x.CatFID == id).ToList();
        }


        private void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Items;

        }

        private void DataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                Button btn = sender as Button;
                var item = btn.CommandParameter as Models.Items;


                int Quantity = 0;
                var QtyRaw = await DisplayActionSheet("Select Quantity", "Close", "", "1", "2", "3", "4", "5", "6", "7", "10", "15");
                if (QtyRaw != "Close" && QtyRaw != null)
                {
                    Quantity = int.Parse(QtyRaw);
                }
                else
                {
                    await DisplayAlert("Message", "Please select at least 1 quantity.", "Ok");
                    return;
                }

                int index = -1;
                for (int i = 0; i < App.Cart.Count; i++)
                {
                    if (item.ItemID == App.Cart[i].ProductFID)
                    {
                        index = i;
                        var ques = await DisplayAlert("Message", item.ItemName + " is already entered into cart. Do you want to increase the quantity of already entered item?", "Yes", "No");
                        if (ques)
                        {
                            App.Cart[index].UnitQuantity += Quantity;
                            await DisplayAlert("Message", item.ItemName + " quantity increased... ", "Ok");

                        }
                    }
                }

                if (index == -1)
                {
                    App.Cart.Add(new OrderDetail { ProductFID = item.ItemID, UnitQuantity = Quantity });
                    await DisplayAlert("Message", item.ItemName + " is added into cart... ", "Ok");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", " Something went wrong...\n\n Errors details.." + ex.Message, "Ok");
            }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPageControls());

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var item = btn.CommandParameter as Models.Items;
            await DisplayAlert("Item details :",
                "Name : " + item.ItemName +
                "\n Detail : " + item.ItemDetail +
                "\n Rating : " + item.Rating +
                "\n Status : " + item.ItemStatus +
                "\n Sale Price : " +item.SPrice +
                "\n Purchase Price : " +item.PPrice,
                "Ok"

                );
        }
    }
}




