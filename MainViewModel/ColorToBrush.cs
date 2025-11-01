﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Othello.ViewModel
{
    public class ColorToBrush : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            String color = (String)value[0];
            int rowInput = (int)value[1];
            int colomnInput = (int)value[2];
            Random random = new Random();
            SolidColorBrush brushes = new SolidColorBrush(Color.FromArgb(255, 255, 0, 255));
            if (color == "Black")
            {
                brushes = Brushes.Black;
            }
            else if (color == "White")
            {
                brushes = Brushes.White;
            }
            else
            {
                // euclidean_distance
                byte e = (byte)Math.Sqrt(Math.Pow(colomnInput, 2) + Math.Pow(rowInput, 2));
                byte e_reverse = (byte)(11-e);
                byte r = 0;
                byte g = 0;
                byte b = 0;
                if ((rowInput + colomnInput) % 2 == 0)
                {
                    
                    r = (byte)(5    + e * 8 + e_reverse * 0 + (random.Next() % 10) - 5);
                    g = (byte)(125  - e * 3 - e_reverse * 3 + (random.Next() % 10) - 5);
                    b = (byte)(5    + e * 2 + e_reverse * 6 + (random.Next() % 10) - 5);
                    brushes = new SolidColorBrush(Color.FromArgb(255, r, g, b));
                }
                else
                {
                    r = (byte)(5    + e * 8 + e_reverse * 0 + (random.Next() % 10) - 5);
                    g = (byte)(150  - e * 0 - e_reverse * 0 + (random.Next() % 10) - 5);
                    b = (byte)(0    + e * 2 + e_reverse * 6 + (random.Next() % 10) - 5);
                    brushes = new SolidColorBrush(Color.FromArgb(255, r, g, b));
                }
            }
            return brushes;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brushes = (SolidColorBrush)value;
            string color = "Violet";
            if (brushes == System.Windows.Media.Brushes.Black)
            {
                color = "Black";
            }
            else if (brushes == System.Windows.Media.Brushes.White)
            {
                color = "White";
            }
            else
            {
                color = "Green";
            }
            object[] result = new object[3];
            result[0] = color;
            result[1] = 0;
            result[2] = 0;
            return result;
        }
    }
}
