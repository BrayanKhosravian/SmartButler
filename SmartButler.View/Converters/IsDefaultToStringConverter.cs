using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartButler.View.Converters
{
	public class IsDefaultToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isDefault = (bool) value;

			return isDefault ? "Readonly default drink!" : string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}