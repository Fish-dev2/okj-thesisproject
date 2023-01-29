using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NagyProjekt
{
    /// <summary>
    /// Interaction logic for JatekAblak.xaml
    /// </summary>
    public partial class JatekAblak : Window
    {
        public JatekAblak()
        {
            InitializeComponent();
            HUDBeallit();
        }

        private void HUDBeallit()
        {
            mainstack.DataContext = this;
            
        }
    }

    class LoszerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int loszer = (int)value;
            if (loszer == -1)
            {
                return "∞";
            }
            else
            {
                return loszer.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class HelyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ertek = (double)value;
            return ertek + 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class EletSzinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ertek = (double)value;

            if (ertek >= 75)
            {
                return new SolidColorBrush(Colors.Green);
            }
            else if (ertek >=50)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            else if (ertek >= 25)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else
            {
                return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
