using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Popups;
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
    public sealed partial class CarryOutMenu : Page
    {
        public static CarryOutMenu Current;
        MainPage rootPage = MainPage.Current;
        public TableContainer testTable;

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
        public CarryOutMenu()
        {
            this.InitializeComponent();
            Current = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter != null)
            {
                testTable = e.Parameter as TableContainer;
                tableNumber.Text = "Table: " + testTable.tableNumber;
                updateReceiptBox();
                updateGuestBlock();
            }
            else
            {
                testTable = new TableContainer() { tableNumber = 100 };
            }
            setTime();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void entreeMenuButton_IsClicked(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(typeof(EntreeMenu));
        }

        private void BeverageMenuButton_IsClicked(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(typeof(BeverageMenu));
        }

        private void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void CheckoutMenuButton_IsClicked(object sender, RoutedEventArgs e)
        {
            if (testTable.guestList.Count == 0)
            {
                var noCashEntered = new MessageDialog("There are no guests.");
                noCashEntered.Commands.Add(new UICommand("OK"));
                await noCashEntered.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(CheckoutMenu), testTable);
            }
        }

        private void appetizerButton_IsClicked(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(typeof(AppetizerMenu));
        }

        private void dessertsMenu_IsClicked(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(typeof(DessertMenu));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(testPage));
        }

        internal void updateReceiptBox()
        {
            for (int u = 0; u < testTable.guestList.Count; u++)
            {
                for (int i = 0; i < testTable.guestList.ElementAt<Guest>(u).foodItems.Count; i++)
                {
                    if (i == 0)
                    {
                        receiptBox.Text = testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).foodName + "\n";
                        receiptBox.Text += "Quantity: " + testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).quantity + "\n";
                        receiptBox2.Text = "\n" + String.Format("{0:0.00}", testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).cost * testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).quantity).ToString() + "\n" + "\n";
                    }
                    else
                    {
                        if (!receiptBox.Text.Contains(testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).foodName))
                        {
                            receiptBox.Text += testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).foodName + "\n";
                            receiptBox.Text += "Quantity: " + testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).quantity + "\n";
                            receiptBox2.Text += String.Format("{0:0.00}", testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).cost * testTable.guestList.ElementAt<Guest>(u).foodItems.ElementAt<FoodItem>(i).quantity).ToString() + "\n" + "\n";
                        }
                    }
                }
            }
        }

        private void updateScrollviewer(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sender == foodScrollviewer)
            {
                costScrollviewer.ChangeView(foodScrollviewer.HorizontalOffset, foodScrollviewer.VerticalOffset, foodScrollviewer.ZoomFactor);
            }
            else
            {
                foodScrollviewer.ChangeView(costScrollviewer.HorizontalOffset, costScrollviewer.VerticalOffset, costScrollviewer.ZoomFactor);
            }
        }

        private void addGuestButton_IsClicked(object sender, RoutedEventArgs e)
        {
            if (testTable.guestList.Count <= 9)
            {
                Guest temp = new Guest();
                testTable.guestList.Add(temp);
                updateGuestBlock();
                updateReceiptBox();
            }
        }

        private void removeGuestButton_IsClicked(object sender, RoutedEventArgs e)
        {
            if (testTable.guestList.Count > 0)
            {
                testTable.guestList.ElementAt<Guest>(testTable.guestList.Count - 1).foodItems.Clear();
                testTable.guestList.RemoveAt(testTable.guestList.Count - 1);
                updateGuestBlock();
                updateReceiptBox();
            }
        }

        internal void updateGuestBlock()
        {
            guestBlock.Text = "Guest(s): " + testTable.guestList.Count;
        }
    }
}

