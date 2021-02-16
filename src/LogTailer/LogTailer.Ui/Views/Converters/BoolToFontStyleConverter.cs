namespace LogTailer.Ui.Views.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToFontStyleConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            if (value is bool isValue)
            {
                return isValue ? FontStyles.Italic : FontStyles.Normal;
            }

            return FontStyles.Normal;
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
