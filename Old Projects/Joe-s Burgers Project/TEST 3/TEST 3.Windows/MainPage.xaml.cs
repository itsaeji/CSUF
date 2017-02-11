
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TEST_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static MainPage Current;


        public MainPage()
        {
            InitializeComponent();
            Current = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Current.ValidateFile();
            setTime();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private async void LoginButton_IsClicked(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Username or password was incorrect.");
            messageDialog.Commands.Add(new UICommand("OK"));
            bool usernameFound = await searchForEmployee(loginFile, usernameBox.Text);
            if (usernameBox.Text != "" && usernameFound)
            {
                bool passwordFound = await searchForPassword(loginFile, usernameBox.Text, loginPasswordBox.Password);
                if (passwordFound)
                {
                    Frame.Navigate(typeof(TwoButtonMenu));
                }
                else
                {
                    //incorrectLoginText.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                //incorrectLoginText.Visibility = Windows.UI.Xaml.Visibility.Visible;
                await messageDialog.ShowAsync();

            }

        }
        private void RegisterEmployeeButton_IsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddNewEmployee));
        }

        private void ExitButton_IsClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
