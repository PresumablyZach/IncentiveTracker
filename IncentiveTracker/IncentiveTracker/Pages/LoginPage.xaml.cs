using IncentiveTracker.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
 * File: Login
 * Description: Handles the user login, saving the name if it is valid,
 *              and prompting the user if the entry is invalid.
 */

namespace IncentiveTracker
{
    public sealed partial class LoginPage : Page
    {
        // Page varaibles
        private String currentUser;

        // Initialization for Login Page
        public LoginPage()
        {
            this.InitializeComponent();
        }

        /* When the login button is clicked, verifies that the user has entered
         * a valid name (non-null string), and navigates to the home page. If an
         * invalid string is found for the user's name, a dialog prompts them to 
         * enter another name.
         */
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            currentUser = usrName.Text; 

            if (currentUser.Length <= 0)
            {
                DisplayInvalidLoginDialog();
            }
            else
            {
                App.currentUser = currentUser;
                this.Frame.Navigate(typeof(HomePage));
            }
        }

        // Allows User to press enter instead of clicking button
        private void UsrName_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                BtnLogin_Click(this, new RoutedEventArgs());
            }
        }

        // Dialog to prompt the user for proper name entry
        private async void DisplayInvalidLoginDialog()
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
