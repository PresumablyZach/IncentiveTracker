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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IncentiveTracker.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RosterPage : Page
    {
        private List<Person> roster;

        public RosterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            roster = App.roster.OrderBy(o=>o.propName).ToList();

            int max = roster.Count;

            for (int i = 0; i < max; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Content = roster[i].propName;
                rosterList.Items.Add(item);
            }

            base.OnNavigatedTo(e);
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

        private async void ShowAddDialog()
        {
            await AddUser.ShowAsync();
        }

        private void AddUser_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            App.AddToRoster(txtAddName.Text);
            ListViewItem item = new ListViewItem();
            item.Content = txtAddName.Text;
            rosterList.Items.Add(item);
        }
    }
        
}
