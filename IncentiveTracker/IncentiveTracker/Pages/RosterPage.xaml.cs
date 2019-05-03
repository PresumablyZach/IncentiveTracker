using IncentiveTracker.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
 * File: Roster
 * Description: Displays the roster, and allows the user to add and delete from it.
 */

namespace IncentiveTracker.Pages
{
    public sealed partial class RosterPage : Page
    {
        // Roster List
        private List<Person> roster;
        // Observable List of strings for the combobox to bind to
        private ObservableCollection<string> observableRoster = new ObservableCollection<string>();

        public RosterPage()
        {
            this.InitializeComponent();
        }

        // Refreshes the List on navigation to the page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshListView();
            base.OnNavigatedTo(e);
        }

        // Shows the dialog for the add form
        private async void ShowAddDialog()
        {
            await AddUser.ShowAsync();
        }

        // Shows the dialog for the delete form
        private async void ShowDeleteDialog()
        {
            await DeleteUser.ShowAsync();
        }

        // Shows the add form when the menu button is pressed
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ShowAddDialog();
        }

        // Ensures there is no text in the add user form on open
        private void AddUser_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            txtAddName.Text = "";
        }

        // Checks if the change in text has made a non-null string, then enables the add button if it has
        private void TxtAddName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAddName.Text.Length > 0)
            {
                AddUser.IsPrimaryButtonEnabled = true;
            }
            else
            {
                AddUser.IsPrimaryButtonEnabled = false;
            }
        }

        // Adds a new user with the name requested
        private void AddUser_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            App.AddToRoster(txtAddName.Text);
            RefreshListView();
        }

        // Shows the Delete from when the menu button is pressed
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ShowDeleteDialog();
        }

        // Hides the delete form and shows the confirm dialog (can't display 2 dialogs at the same time)
        private void DeleteUser_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DeleteUser.Hide();
            DeleteUserConfirmDialog();
        }

        // Dialog to confirm the user wants to delete a member
        private async void DeleteUserConfirmDialog()
        {
            // Creates the Dialog
            ContentDialog deleteUserDialog = new ContentDialog
            {
                Title = "Delete " + comboRoster.SelectedValue.ToString() + " permanently?",
                Content = "If you delete " + comboRoster.SelectedValue.ToString() + " from the roster, you won't be able to recover them. Do you want to delete them?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            // Waits for the user response and handles accordingly
            ContentDialogResult result = await deleteUserDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                App.DeleteFromRoster(comboRoster.SelectedValue.ToString());
                RefreshListView();
            }
            else { }
        }

        // Refreshes the ListView with the roster from the main App file
        private void RefreshListView()
        {
            // Sorts the roster alphabetically
            roster = App.roster.OrderBy(o => o.propName).ToList();

            // Clears the ListView and ComboBox
            rosterList.Items.Clear();
            observableRoster.Clear();

            int max = roster.Count;

            // Adds each member from the roster to the List
            for (int i = 0; i < max; i++)
            {
                ListViewItem item = new ListViewItem();

                item.Content = roster[i].propName;
                rosterList.Items.Add(item);
                observableRoster.Add(roster[i].propName);
            }
        }

        

    }
        
}
