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
    public sealed partial class TwoButtonMenu : Page
    {
        MainPage rootPage = MainPage.Current;

        // Load Two-Button Menu
        public TwoButtonMenu()
        {
            this.InitializeComponent();
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public void setTime()
        {
            var formatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day dayofweek year hour minute second");
            DateTime dateToFormat = DateTime.Now;
            var mydate = formatter.Format(dateToFormat);
            textBlock1.Text = mydate.ToString();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            setTime();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            setTime();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        // Event-handlers
        private void CarryOutButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CarryOutMenu));
        }

        private void DineInButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TableLayout));
        }

        private void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
