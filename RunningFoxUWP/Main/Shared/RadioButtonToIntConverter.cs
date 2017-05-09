using System;
using Windows.UI.Xaml.Data;

namespace Main.Shared
{
  public class RadioButtonToIntConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int parameterNumber, currentValue;
            if (parameter != null)
            {
                parameterNumber = System.Convert.ToInt32(value);
                currentValue = System.Convert.ToInt32(parameter);

                if (parameterNumber == currentValue)
                    return true;
            }

            return false;
        }

      

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var para = System.Convert.ToInt32(parameter);
            var isChecked = System.Convert.ToBoolean(value);
            return isChecked ? para : 0;
        }       
    }
}
