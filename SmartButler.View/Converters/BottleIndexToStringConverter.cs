using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartButler.View.Converters
{
	class BottleIndexToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var position = (int)value;

			return position <= 6 && position >= 1 ? $"Bottle position: {position.ToString()}" : "Bottle position: Not selected";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
