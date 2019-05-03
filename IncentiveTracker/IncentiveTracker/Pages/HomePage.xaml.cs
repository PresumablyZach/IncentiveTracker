using IncentiveTracker.Pages;
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
 * File: Home
 * Description: The hub of the application, displays current incentives, and has menus
 *              for user actions such as creating new incentives, and editing old ones
 */

namespace IncentiveTracker
{

    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        // Loads all the necessary global variables from the main App file
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);
        }

        private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the initially seleced item to the incentive page
            foreach (NavigationViewItemBase item in NavigationViewControl.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home_Page")
                {
                    NavigationViewControl.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(IncentivePage));
            NavigationViewControl.PaneTitle = App.currentUser;
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else 
            {
                TextBlock ItemContent = args.InvokedItem as TextBlock;
                if (ItemContent != null)
                {
                    switch (ItemContent.Tag)
                    {
                        case "Nav_Home":
                            contentFrame.Navigate(typeof(IncentivePage));
                            break;
                        case "Nav_Roster":
                            contentFrame.Navigate(typeof(RosterPage));
                            break;
                        case "Nav_Switch":
                            contentFrame.Navigate(typeof(SwitchPage));
                            break;
                    }
                }
            }
        }
    }
}
