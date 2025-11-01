using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Othello.ViewModel
{
    public class ColorToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String color = (String)value;
            if (color == "Black")
            {
                return System.Windows.Media.Brushes.Black;
            }
            else if (color == "White")
            {
                return System.Windows.Media.Brushes.White;
            }
            else
            {
                return System.Windows.Media.Brushes.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brushes = (SolidColorBrush)value;
            if (brushes == System.Windows.Media.Brushes.Black)
            {
                return "Black";
            }
            else if (brushes == System.Windows.Media.Brushes.White)
            {
                return "White";
            }
            else if (brushes == System.Windows.Media.Brushes.Green)
            {
                return "Green";
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
