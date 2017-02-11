using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

namespace TEST_3
{

    public partial class MainPage : Page
    {
        public const string APP_NAME = "Joe's Burgers";

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

        public class FoodButton : Button
        {
            public string foodName { get; set; }
            public double cost { get; set; }
        }

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Two Button Menu", ClassType = typeof(TwoButtonMenu)},
            new Scenario() { Title = "Table Layout", ClassType = typeof(TableLayout)},
            new Scenario() { Title = "Carry Out Menu", ClassType = typeof(CarryOutMenu)},
            new Scenario() { Title = "Checkout Menu", ClassType = typeof(CheckoutMenu)},
            new Scenario() { Title = "Dessert Menu", ClassType = typeof(DessertMenu)},
            new Scenario() { Title = "Entree Menu", ClassType = typeof(EntreeMenu)},
            new Scenario() { Title = "Beverage Menu", ClassType = typeof(BeverageMenu)},
        };

        public List<TableContainer> containers = new List<TableContainer>
        {
            new TableContainer(){ tableNumber = 1},
            new TableContainer(){ tableNumber = 2},
            new TableContainer(){ tableNumber = 3},
            new TableContainer(){ tableNumber = 4},
            new TableContainer(){ tableNumber = 5},
            new TableContainer(){ tableNumber = 6},
            new TableContainer(){ tableNumber = 7},
            new TableContainer(){ tableNumber = 8},
            new TableContainer(){ tableNumber = 9},
            new TableContainer(){ tableNumber = 10},
            new TableContainer(){ tableNumber = 11},
            new TableContainer(){ tableNumber = 12},
            new TableContainer(){ tableNumber = 13},
            new TableContainer(){ tableNumber = 14},
            new TableContainer(){ tableNumber = 15},
            new TableContainer(){ tableNumber = 16},
            new TableContainer(){ tableNumber = 17},
            new TableContainer(){ tableNumber = 18},
            new TableContainer(){ tableNumber = 19}
        };

        public const string filename = "loginDatabase.txt";
        public StorageFile loginFile = null;

        internal async void ValidateFile()
        {
            loginFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
        }

        public async Task<bool> searchForEmployee(StorageFile file, string username)
        {
            Stream temp = await file.OpenStreamForReadAsync();
            StreamReader toBeRead = new StreamReader(temp);
            string test = null;
            bool found = false;
            while(!found && !toBeRead.EndOfStream)
            {
                test = await toBeRead.ReadLineAsync();
                if(test == username)
                {
                    found = true;
                }
            }
            return found;
        }
        public async Task<bool> searchForPassword(StorageFile file, string username, string password)
        {
            Stream temp = await file.OpenStreamForReadAsync();
            StreamReader toBeRead = new StreamReader(temp);
            string test = null;
            bool found = false;
            while (!found && !toBeRead.EndOfStream)
            {
                test = await toBeRead.ReadLineAsync();
                if (test == username)
                {
                    test = await toBeRead.ReadLineAsync();
                    if(test == password)
                    {
                        found = true;
                    }
                }
            }
            return found;
        }

        public void BackButton_IsClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }



    public class Scenario
    {
        public string Title { get; set; }

        public Type ClassType { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }

    public class Guest
    {
        public List<FoodItem> foodItems = new List<FoodItem> {};

        public int count;
    }

    public class TableContainer
    {
        public int tableNumber { get; set; }

        public List<Guest> guestList = new List<Guest> { };

        public double totalCost { get; set; }

        public override string ToString()
        {
            return tableNumber.ToString();
        }
    }

    public class FoodItem
    {
        public string foodName { get; set; }

        public double cost { get; set; }

        public string foodType { get; set; }

        public int quantity { get; set; }

        public override string ToString()
        {
            return foodName;
        }
     }
}
