using System;
using System.Globalization;
using System.Windows.Data;

using TuwenDayinDian.Models;

namespace TuwenDayinDian.Converters
{
    public class FirstCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MenuItem menuItem)
            {
                if (string.IsNullOrWhiteSpace(menuItem.ImageIcon))
                {
                    return menuItem.MenuText.Substring(0, 1);
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
