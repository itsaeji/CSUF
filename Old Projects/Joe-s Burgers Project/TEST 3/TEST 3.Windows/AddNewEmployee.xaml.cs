using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TEST_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewEmployee : Page
    {
        MainPage rootPage = MainPage.Current;

        public AddNewEmployee()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage.ValidateFile();
        }

        private async void RegisterNewUserButton_IsClicked(object sender, RoutedEventArgs e)
        {
            usernameIsTakenBlock.Visibility = Visibility.Collapsed;
            passwordsMustMatchText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            StorageFile file = rootPage.loginFile;
            bool testBool = await rootPage.searchForEmployee(file, usernameBox.Text);
            if(testBool)
            {
                usernameIsTakenBlock.Visibility = Visibility.Visible;
            }
            else
            {
                if(passwordBox.Password == reenterPasswordBox.Password)
                {
                    await Windows.Storage.FileIO.AppendTextAsync(file, "\n" + usernameBox.Text + "\n" + passwordBox.Password);
                    this.Frame.GoBack();
                }
                else
                {
                    passwordsMustMatchText.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        private void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
