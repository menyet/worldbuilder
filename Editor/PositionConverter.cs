﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Editor
{
    class PositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) values[0]*(double) values[1] + (double) values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
