using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;

/*
 * Author: Zach Gyorffy
 * Version: 1.0
 * Application: IncentiveTracker
 * File: App
 * Description: Handles launch, suspension, failure, etc. events.
 *              Also holds the global variables needed for the app.
 */

namespace IncentiveTracker
{
    sealed partial class App : Application
    {
        public static String currentUser;
        public static List<Person> roster;

        // Initialize the Application
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        // Normal launch by user
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(LoginPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }

            //Reads in the roster file and creates the roster list

            roster = new List<Person>();
            LoadRoster();
            /*roster.Add(new Person("Zach"));
            roster.Add(new Person("Ivonne"));
            roster.Add(new Person("Kerri-Ann"));
            roster.Add(new Person("Victoria"));
            roster.Add(new Person("Jesse"));
            roster.Add(new Person("Tim"));
            roster.Add(new Person("Laura"));*/

        }

        // Invoked if/when navigation to a certain page fails
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /* Invoked when the application is Suspended
         * Application state is saved without knowing whether the application will be 
         * terminated or resumed with the contents of memory still intact.
         */
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            SaveRoster();
            deferral.Complete();
        }

        private static async void SaveRoster()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync("roster.txt", CreationCollisionOption.OpenIfExists);
            StorageFile rosterFile = await localFolder.GetFileAsync("roster.txt");

            string rawRoster = "";

            for (int i = 0; i < roster.Count; i++)
            {
                string name = roster[i].propName;
                int pointVal = roster[i].propPointVal;
                rawRoster = String.Concat(rawRoster, (name +","+pointVal+";"));
            }

            await FileIO.WriteTextAsync(rosterFile, rawRoster);
            Debug.WriteLine("RAW: " + rawRoster + "\n" + "File Contents: " + await localFolder.GetFileAsync("roster.txt"));

        }

        private async void LoadRoster()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync("roster.txt", CreationCollisionOption.OpenIfExists);
            StorageFile rosterFile = await storageFolder.GetFileAsync("roster.txt");

            string text = await FileIO.ReadTextAsync(rosterFile);

            string[] rawPeople = text.Split(';');
            for (int i = 0; i < rawPeople.Length - 1; i++)
            {
                string[] rawPerson = rawPeople[i].Split(',');
                string name = rawPerson[0];
                int pointVal = 0;
                try
                {
                    pointVal = Convert.ToInt32(rawPerson[1]);
                }
                catch (FormatException) { }
                catch (OverflowException) { }

                roster.Add(new Person(name));
                roster[roster.Count - 1].propPointVal = pointVal;

            }
        }

        public static void AddToRoster (string name)
        {
            roster.Add(new Person(name));
            SaveRoster();
        }

        public static void DeleteFromRoster (string name)
        {
            for (int i = 0; i < roster.Count; i++)
            {
                if (roster[i].propName == name)
                {
                    roster.RemoveAt(i);
                }
            }
            SaveRoster();
        }
    }
}
