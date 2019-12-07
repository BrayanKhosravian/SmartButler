using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartButler.Framework.Common;

namespace SmartButler.Framework.Resources
{
	public static class ResourceManager
	{
		public static byte[] GetImageAsBytes(string resourcePath)
		{
			if (string.IsNullOrWhiteSpace(resourcePath))
				throw ExceptionFactory.Get<ArgumentException>("'resourcePath' is null or has whitespaces");

			var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "SmartButler.Framework");

			byte[] byteImage;
			using (var stream = assembly?.GetManifestResourceStream(resourcePath))
			{
				if (stream == null)
					return null;

				var length = stream.Length;
				byteImage = new byte[length];
				stream.Read(byteImage, 0, (int)length);
			}

			return byteImage;
		}
	}
}
