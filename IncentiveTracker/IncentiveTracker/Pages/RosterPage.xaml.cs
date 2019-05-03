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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IncentiveTracker.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RosterPage : Page
    {
        private List<Person> roster;
        private ObservableCollection<string> observableRoster = new ObservableCollection<string>();
        private MenuFlyout rightClickMenu = new MenuFlyout();

        public RosterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshListView();
            base.OnNavigatedTo(e);
        }

        private async void ShowAddDialog()
        {
            await AddUser.ShowAsync();
        }

        private async void ShowDeleteDialog()
        {
            await DeleteUser.ShowAsync();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ShowAddDialog();
        }

        private void AddUser_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            txtAddName.Text = "";
        }

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

        private void AddUser_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            App.AddToRoster(txtAddName.Text);
            RefreshListView();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ShowDeleteDialog();
        }

        private void DeleteUser_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DeleteUser.Hide();
            DeleteUserConfirmDialog();
        }

        private async void DeleteUserConfirmDialog()
        {
            ContentDialog deleteUserDialog = new ContentDialog
            {
                

                Title = "Delete " + comboRoster.SelectedValue.ToString() + " permanently?",
                Content = "If you delete " + comboRoster.SelectedValue.ToString() + " from the roster, you won't be able to recover them. Do you want to delete them?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

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
