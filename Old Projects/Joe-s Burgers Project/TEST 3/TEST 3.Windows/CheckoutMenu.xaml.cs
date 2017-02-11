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
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Printing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TEST_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    
    public sealed partial class CheckoutMenu : Page
    {
        TableContainer checkoutTable;

        public CheckoutMenu()
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
            if (e.Parameter != null)
            {
                checkoutTable = e.Parameter as TableContainer;
                tableTextblock.Text = "Table: " + checkoutTable.tableNumber;
                
                updateReceiptBox();
                updateTotal();
            }
            notificationTextbox.Text += "Subtotal:\nTax:\nGrand Total:\n";
            double total = checkoutTable.totalCost;
            costsTextbox.Text += String.Format("{0:0.00}", total) + "\n";
            double taxes = Math.Round(total * .08, 2);
            costsTextbox.Text += String.Format("{0:0.00}", taxes) + "\n";
            double grandTotal = Math.Round(total + taxes, 2);
            costsTextbox.Text += String.Format("{0:0.00}", grandTotal) + "\n";
            setTime();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void updateTotal()
        {
            double total = checkoutTable.totalCost;
            double taxes = Math.Round(total * .08, 2);
            double grandTotal = Math.Round(total + taxes, 2);
            totalTextblock.Text = String.Format("{0:0.00}", grandTotal);
        }

        private void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void updateReceiptBox()
        {
            for (int i = 0; i < checkoutTable.guestList.ElementAt<Guest>(0).foodItems.Count; i++)
            {
                if (i == 0)
                {

                    checkoutReceiptBox.Text = checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).foodName + "\n";
                    checkoutReceiptBox.Text += "Quantity: " + checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity + "\n";
                    costTextbox.Text = "\n" + String.Format("{0:0.00}", checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).cost * checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity).ToString() + "\n" + "\n";
                    checkoutTable.totalCost = (checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).cost * checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity);
                }
                else
                {
                    if (!checkoutReceiptBox.Text.Contains(checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).foodName))
                    {
                        checkoutReceiptBox.Text += checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).foodName + "\n";
                        checkoutReceiptBox.Text += "Quantity: " + checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity + "\n";
                        costTextbox.Text += String.Format("{0:0.00}", checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).cost * checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity).ToString() + "\n" + "\n";
                        checkoutTable.totalCost += (checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).cost * checkoutTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity);
                    }
                }
            }
        }

        private void tenKeyButton_IsClicked(object sender, RoutedEventArgs e)
        {
            var testButton = sender as Button;
            string testString = (string)testButton.Content;
            if (chargedTextBlock.Text == "0.00")
            {
                chargedTextBlock.Text = testString;
            }
            else
            {
                chargedTextBlock.Text += testString;
            }
        }

        private async void cashButton_IsClicked(object sender, RoutedEventArgs e)
        {
            double cash = Convert.ToDouble(chargedTextBlock.Text);
            double finalTotal = Math.Round(Convert.ToDouble(totalTextblock.Text) - cash,2);
            if(finalTotal <= 0)
            {
                if (cash != 0)
                {
                    updateNotificationTextbox("\tCash Tendered:\nChange:");
                    updateCostsTextbox(String.Format("{0:0.00}", cash) + "\n" + String.Format("{0:0.00}", finalTotal * - 1));
                    totalTextblock.Text = "0.00";
                    chargedTextBlock.Text = "0.00";
                }
                else
                {
                    var noCashEntered = new MessageDialog("Please enter an amount.");
                    noCashEntered.Commands.Add(new UICommand("OK"));
                    await noCashEntered.ShowAsync();
                }
            }
            else
            {
                if (cash != 0)
                {
                    updateNotificationTextbox("\tCash Tendered:\n");
                    updateCostsTextbox(String.Format("{0:0.00}", cash) + "\n");
                    totalTextblock.Text = Convert.ToString(finalTotal);
                    chargedTextBlock.Text = "0.00";
                }
                else
                {
                    var noCashEntered = new MessageDialog("Please enter an amount.");
                    noCashEntered.Commands.Add(new UICommand("OK"));
                    await noCashEntered.ShowAsync();
                }
            }
        }

        private void updateCostsTextbox(string newLine)
        {
            costsTextbox.Text += newLine;
        }

        private void updateNotificationTextbox(string nextLine)
        {
            notificationTextbox.Text += nextLine;
        }

        private void splitByN_IsClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private async void closeButton_IsClicked(object sender, RoutedEventArgs e)
        {
            if (totalTextblock.Text == "0.00")
            {
                checkoutTable.guestList.ElementAt<Guest>(0).foodItems.RemoveRange(0, checkoutTable.guestList.ElementAt<Guest>(0).foodItems.Count);
                checkoutTable.totalCost = 0;
                checkoutTable.guestList.Clear();
                this.Frame.Navigate(typeof(TwoButtonMenu));
            }
            else
            {
                var transactionIncomplete = new MessageDialog("Transaction has not been completed. Please complete the transaction.");
                transactionIncomplete.Commands.Add(new UICommand("OK"));
                await transactionIncomplete.ShowAsync();
            }
        }

        private void clearButton_IsClicked(object sender, RoutedEventArgs e)
        {
            chargedTextBlock.Text = "0.00";
        }

        private void exactChangeButton_IsClicked(object sender, RoutedEventArgs e)
        {
            chargedTextBlock.Text = totalTextblock.Text;
        }

        private void splitEvenlyButton_IsClicked(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = numberToSplit.SelectedValue as ComboBoxItem;
            int evenSplit = Convert.ToInt32(Convert.ToString(temp.Content));
            chargedTextBlock.Text = String.Format("{0:0.00}", Convert.ToDouble(totalTextblock.Text) / evenSplit);
            splitFlyout.Hide();
        }

        private async void creditCardButton_IsClicked(object sender, RoutedEventArgs e)
        {
                        double amountCharged = 0;
            if (chargedTextBlock.Text != "0.00")
            {
                amountCharged = Convert.ToDouble(chargedTextBlock.Text);
            }
            else
            {
                amountCharged = Convert.ToDouble(totalTextblock.Text);
            }
            double finalTotal = Math.Round(Convert.ToDouble(totalTextblock.Text) - amountCharged, 2);
            if (amountCharged != 0)
            {
                if (finalTotal <= 0)
                {
                    if (amountCharged > Convert.ToDouble(totalTextblock.Text))
                    {
                        var amountGreater = new MessageDialog("Amount to be charged on credit card cannot exceed total.");
                        amountGreater.Commands.Add(new UICommand("OK"));
                        await amountGreater.ShowAsync();
                    }
                    else if (chargedTextBlock.Text == "0.00")
                    {
                        updateNotificationTextbox("\tCredit Card Charged:\n");
                        updateCostsTextbox(String.Format("{0:0.00}", amountCharged) + "\n");
                        totalTextblock.Text = "0.00";
                        chargedTextBlock.Text = "0.00";
                    }
                    else
                    {
                        updateNotificationTextbox("\tCredit Card Charged:\n");
                        updateCostsTextbox(Convert.ToString(amountCharged));
                        chargedTextBlock.Text = "0.00";
                    }
                }
                else
                {
                    updateNotificationTextbox("\tCredit Card Charged:\n");
                    updateCostsTextbox(String.Format("{0:0.00}", amountCharged) + "\n");
                    totalTextblock.Text = Convert.ToString(finalTotal);
                    chargedTextBlock.Text = "0.00";
                }
            }
            else
            {
                var amountGreater = new MessageDialog("Transaction is complete.");
                amountGreater.Commands.Add(new UICommand("OK"));
                await amountGreater.ShowAsync();
            }
        }

        private void discountPercentButton_IsClicked(object sender, RoutedEventArgs e)
        {
            double finalTotal = Convert.ToDouble(totalTextblock.Text);
            finalTotal = finalTotal - (finalTotal * .1);
            totalTextblock.Text = String.Format("{0:0.00}",finalTotal);
        }

        private async void checkButton_IsClicked(object sender, RoutedEventArgs e)
        {
            double amountCharged = 0;
            if (chargedTextBlock.Text != "0.00")
            {
                amountCharged = Convert.ToDouble(chargedTextBlock.Text);
            }
            else
            {
                amountCharged = Convert.ToDouble(totalTextblock.Text);
            }
            double finalTotal = Math.Round(Convert.ToDouble(totalTextblock.Text) - amountCharged, 2);
            if (amountCharged != 0)
            {
                if (finalTotal <= 0)
                {
                    if (amountCharged > Convert.ToDouble(totalTextblock.Text))
                    {
                        var amountGreater = new MessageDialog("Amount to be charged on check cannot exceed total.");
                        amountGreater.Commands.Add(new UICommand("OK"));
                        await amountGreater.ShowAsync();
                    }
                    else if (chargedTextBlock.Text == "0.00")
                    {
                        updateNotificationTextbox("\tCheck Charged:\n");
                        updateCostsTextbox(String.Format("{0:0.00}", amountCharged) + "\n");
                        totalTextblock.Text = "0.00";
                        chargedTextBlock.Text = "0.00";
                    }
                    else
                    {
                        updateNotificationTextbox("\tCheck Charged:\n");
                        updateCostsTextbox(Convert.ToString(amountCharged));
                        chargedTextBlock.Text = "0.00";
                    }
                }
                else
                {
                    updateNotificationTextbox("\tCheck Charged:\n");
                    updateCostsTextbox(String.Format("{0:0.00}", amountCharged) + "\n");
                    totalTextblock.Text = Convert.ToString(finalTotal);
                    chargedTextBlock.Text = "0.00";
                }
            }
            else
            {
                var amountGreater = new MessageDialog("Transaction is complete.");
                amountGreater.Commands.Add(new UICommand("OK"));
                await amountGreater.ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double temp = Convert.ToDouble(discountTextbox.Text);
            totalTextblock.Text = String.Format("{0:0.00}", Convert.ToDouble(totalTextblock.Text) - Convert.ToDouble(totalTextblock.Text) * (temp / 100));
        }
    }
}
