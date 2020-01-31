using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartButler.View.Converters
{
	public class IsInputValidColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var isValid = (bool) value;

			return isValid ? Color.Default : Color.LightCoral;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var color = (Color)value;
			if(color == Color.Default)
				return true;
			else return false;
		}
	}
}
