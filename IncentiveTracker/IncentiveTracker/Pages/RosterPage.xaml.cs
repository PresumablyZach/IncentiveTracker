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
            roster = new List<Person>(App.roster);

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
            
        }

        private void BtnAdd_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFlyAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
