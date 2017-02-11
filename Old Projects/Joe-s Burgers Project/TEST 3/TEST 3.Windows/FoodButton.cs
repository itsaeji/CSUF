using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace TEST_3
{
    public sealed class FoodButton : Button
    {
        public string foodName { get; set; }

        public double cost { get; set; }

        public TextBox description { get; set; }

        public FoodButton()
        {
            this.DefaultStyleKey = typeof(FoodButton);
        }
    }
}
