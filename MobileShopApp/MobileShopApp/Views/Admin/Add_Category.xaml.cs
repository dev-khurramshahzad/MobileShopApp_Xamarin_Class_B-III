﻿using Firebase.Database.Query;
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
    public partial class Add_Category : ContentPage
    {
        private MediaFile _mediaFile;
        public static string PicPath = "image_picker.png";
        public Add_Category()
        {
            InitializeComponent();
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


                if (string.IsNullOrEmpty(txtCatName.Text) || string.IsNullOrEmpty(txtDetails.Text))
                {
                    await DisplayAlert("Error", "Please fill all required fields and try again", "OK");
                    return;
                }


                var check = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).FirstOrDefault(x => x.Object.Name == txtCatName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.Name + " is already exsits", "OK");
                    return;
                }

                LoadingInd.IsRunning = true;


                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Categories").OnceAsync<Categories>()).Max(a => a.Object.CatID);
                    NewID = ++LastID;
                }


                var StoredImageURL = await App.FirebaseStorage
                .Child("CatImages")
                .Child(NewID.ToString() +"_" + txtCatName.Text + ".jpg")
                .PutAsync(_mediaFile.GetStream());



                Categories c = new Categories()
                {
                    CatID = NewID,
                    Name = txtCatName.Text,
                    Details = txtDetails.Text,
                    Image = StoredImageURL


                };

                await App.firebaseDatabase.Child("Categories").PostAsync(c);

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