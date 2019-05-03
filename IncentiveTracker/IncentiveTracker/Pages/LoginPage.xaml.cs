using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

/*
 * Author: Zach Gyorffy
 * Version: 1.0
 * Application: IncentiveTracker
 */

namespace IncentiveTracker
{
    public sealed partial class LoginPage : Page
    {
        private String currentUser;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            currentUser = usrName.Text; 

            if (currentUser.Length <= 0)
            {
                DisplayIvalidLoginDialog();
            }
            else
            {
                App.currentUser = currentUser;
                this.Frame.Navigate(typeof(HomePage));
            }
        }

        private async void DisplayIvalidLoginDialog()
        {
            ContentDialog invalidLoginDialog = new ContentDialog
            {
                Title = "Invalid Login Attempt",
                Content = "You haven't entered a name. Please enter a name whose length is greater than 0.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await invalidLoginDialog.ShowAsync();
        }
    }
}
