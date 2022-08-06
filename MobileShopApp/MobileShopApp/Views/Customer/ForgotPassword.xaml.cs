using MobileShopApp.Models;
using MobileShopApp.Views.Customer;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickMart.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {

        public ForgotPassword()
        {
            InitializeComponent();
        }


      

        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    Required Field Validator =======================================================================
            //    if (string.IsNullOrEmpty(txtEmailReset.Text))
            //    {
            //        await DisplayAlert("Message", "Please enter email", "Ok");
            //        return;
            //    }

            //    var check = App.db.Table<Models.Users>().FirstOrDefault(x => x.Email == txtEmailReset.Text);

            //    if (check == null)
            //    {
            //        await DisplayAlert("Message", "The email you have entered is not registered.", "Ok");
            //        return;
            //    }

            //    Progress.IsRunning = true;

            //    EMAIL SENDING ================================================================

            //   MailMessage mail = new MailMessage();
            //    mail.To.Add(check.Email);
            //    mail.From = new MailAddress("martquick30@gmail.com", "Password Forgotton", System.Text.Encoding.UTF8);
            //    mail.Subject = "Password Forgot Request";
            //    mail.SubjectEncoding = System.Text.Encoding.UTF8;

            //    mail.Body = "Dear customer! Your current login details are as follows : <br><br><br>Username = " + check.Email + " <br>Password = " + check.Password + " <br><br>Quick Mart Team";
            //    mail.BodyEncoding = System.Text.Encoding.UTF8;
            //    mail.IsBodyHtml = true;
            //    mail.Priority = MailPriority.High;

            //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //    client.Credentials = new System.Net.NetworkCredential("martquick30@gmail.com", "mart3048");
            //    client.EnableSsl = true;

            //    client.Send(mail);

            //    await DisplayAlert("Message", "Your login details are sent to your email address. Please find that in your inbox", "Ok");
            //    await Navigation.PopAsync();


            //    Progress.IsRunning = false;
            //}
            //catch (Exception ex)
            //{
            //    Progress.IsRunning = false;
            //    await DisplayAlert("Message", "Somthing went wrong. This may be a problem with internet or application. Please ensure that you have a working internet connectiony . \nError details : " + ex.Message, "Ok");
            //}
        }

       
    }
}