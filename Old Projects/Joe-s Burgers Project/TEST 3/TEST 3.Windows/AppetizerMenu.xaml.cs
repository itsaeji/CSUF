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
    public sealed partial class AppetizerMenu : Page
    {
        MainPage rootPage = MainPage.Current;
        CarryOutMenu carryOutPage = CarryOutMenu.Current;

        public AppetizerMenu()
        {
            this.InitializeComponent();
        }

        private void FoodItem_Clicked(object sender, RoutedEventArgs e)
        {
            var foodButton = sender as FoodButton;
            FoodItem newItem = new FoodItem() { foodName = foodButton.foodName, cost = Math.Round(foodButton.cost, 2), foodType = "Appetizer" };
            if (carryOutPage.testTable.guestList.Count == 0)
            {
                Guest temp = new Guest();
                temp.foodItems.Add(newItem);
                temp.foodItems.ElementAt<FoodItem>(0).quantity = 1;
                carryOutPage.testTable.guestList.Add(temp);
            }
            else
            {
                for (int i = 0; i < carryOutPage.testTable.guestList.ElementAt<Guest>(0).foodItems.Count; i++)
                {
                    if (newItem.foodName == carryOutPage.testTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).foodName)
                    {
                        carryOutPage.testTable.guestList.ElementAt<Guest>(0).foodItems.ElementAt<FoodItem>(i).quantity++;
                    }
                    else if (i == carryOutPage.testTable.guestList.ElementAt<Guest>(0).foodItems.Count - 1)
                    {
                        carryOutPage.testTable.guestList.ElementAt<Guest>(0).foodItems.Add(newItem);
                    }
                }
            }
            carryOutPage.updateReceiptBox();
            carryOutPage.updateGuestBlock();
        }
    }
}
