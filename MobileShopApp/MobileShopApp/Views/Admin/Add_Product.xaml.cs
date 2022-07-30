using Firebase.Database.Query;
using MobileShopApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class Add_Product : ContentPage
    {
        private MediaFile _mediaFile;
        public static string PicPath = "image_picker.png";
        public Add_Product()
        {
            InitializeComponent();
            LoadData();
        }
        
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).Select(x => new Categories
            {
                Name = x.Object.Name,
            }).ToList();


            var refinedList = firebaseList.Select(x => x.Name).ToList();
            ddlCat.ItemsSource = refinedList;

        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Taking Image...", "OK");
                        return;
                    }

                    _mediaFile = SelectedImg;


                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    _mediaFile = SelectedImg;

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }
        }

        private async void btnCat_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductSalePrice.Text))
                {
                    await DisplayAlert("Error", "Please fill all required fields and try again", "OK");
                    return;
                }

                if (ddlStatus.SelectedItem == null)
                {
                    await DisplayAlert("Error", "Please select product status  and try again", "OK");
                    return;
                }

                if (ddlCat.SelectedItem == null)
                {
                    await DisplayAlert("Error", "Please select product category  and try again", "OK");
                    return;
                }


                var check = (await App.firebaseDatabase.Child("Items").OnceAsync<Items>()).FirstOrDefault(x => x.Object.ItemName == txtProductName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.ItemName + " is already exsits", "OK");
                    return;
                }

                LoadingInd.IsRunning = true;


                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("Items").OnceAsync<Items>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Items").OnceAsync<Items>()).Max(a => a.Object.ItemID);
                    NewID = ++LastID;
                }

                List<Categories> cats = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).Select(x => new Categories
                {
                    CatID = x.Object.CatID,
                    Name = x.Object.Name,
                }).ToList();

                int selected = cats[ddlCat.SelectedIndex].CatID;



                var StoredImageURL = await App.FirebaseStorage
                .Child("ItemImages")
                .Child(NewID.ToString() + "_" + txtProductName.Text + ".jpg")
                .PutAsync(_mediaFile.GetStream());



                Items c = new Items()
                {
                    ItemID = NewID,
                    ItemName = txtProductName.Text,
                    ItemStatus = ddlStatus.SelectedItem.ToString(),
                    PPrice = float.Parse(txtProductPurchasePrice.Text),
                    Quantity = int.Parse(txtProductQty.Text),
                    CatFID = selected,
                    SPrice = float.Parse(txtProductSalePrice.Text),
                    Rating = txtProductRating.Text,
                    ItemDetail = txtProductDetails.Text,
                    ItemImage = StoredImageURL


                };

                await App.firebaseDatabase.Child("Items").PostAsync(c);

                LoadingInd.IsRunning = false;


                await DisplayAlert("Success", "Category Added", "OK");
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;

                await DisplayAlert("Error", "Something went wrong, please try again later.\nError: " + ex.Message, "OK");

            }
        }
    }
}