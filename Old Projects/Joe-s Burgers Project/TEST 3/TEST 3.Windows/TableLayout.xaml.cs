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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TEST_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TableLayout : Page
    {
        MainPage rootPage = MainPage.Current;

        public TableLayout()
        {
            this.InitializeComponent();
        }

        private void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Table_IsClicked(object sender, RoutedEventArgs e)
        {
            var testButton = sender as Button;
            string testString = (string)testButton.Content;
            int testInt = Convert.ToInt32(testString);
            TableContainer toSend = rootPage.containers.ElementAt<TableContainer>(testInt - 1);
            this.Frame.Navigate(typeof(CarryOutMenu), toSend);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int count = 0;
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table1.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table1.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table2.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table2.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table3.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table3.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table4.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table4.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table5.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table5.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table6.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table6.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table7.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table7.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table8.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table8.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table9.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table9.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table10.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table10.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table11.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table11.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table12.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table12.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table13.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table13.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table14.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table14.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table15.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table15.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table16.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table16.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table17.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table17.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table18.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table18.Background.Opacity = 1;
                count++;
            }
            if (rootPage.containers.ElementAt<TableContainer>(count).guestList.Count == 0)
            {
                Table19.Background.Opacity = 0;
                count++;
            }
            else
            {
                Table19.Background.Opacity = 1;
                count++;
            }
        }

    }
} 
