using System;
using Windows.UI.Xaml.Data;

namespace Main.Shared
{
    public class BoolToYesNoConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "Yes" : "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
