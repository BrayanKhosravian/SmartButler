using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SmartButler.View.Converters
{
	public class ByteArrayToImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var byteImage = value as byte[];
			if (byteImage == null) return null;

			return ImageSource.FromStream(() => new MemoryStream(byteImage));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new byte[0];
		}
	}
}
