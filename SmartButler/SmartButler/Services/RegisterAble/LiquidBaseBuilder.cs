using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using SmartButler.Models;
using Xamarin.Forms;

namespace SmartButler.Services.RegisterAble
{
	public interface ILiquidBaseBuilder<TLiquidBase> where TLiquidBase : LiquidBase
	{
		LiquidBaseBuilder<TLiquidBase> Default();

		LiquidBaseBuilder<TLiquidBase> Default(string name, string partialResource, Type resolvingType);

		LiquidBaseBuilder<TLiquidBase> SetName(string name);
		LiquidBaseBuilder<TLiquidBase> SetImageSource(string partialResource, Type resolvingType);

		TLiquidBase Build();

	}

	public abstract class LiquidBaseBuilder<TLiquidBase> 
		where TLiquidBase : LiquidBase
	{
		protected string Name;
		protected byte[] ByteImage;
		protected ImageSource ActualImage;
		public abstract TLiquidBase Build();

		public virtual LiquidBaseBuilder<TLiquidBase> Default()
		{
			Name = null;
			ByteImage = null;
			ActualImage = null;

			return this;
		}

		public virtual LiquidBaseBuilder<TLiquidBase> Default(string name, string partialResource, Type resolvingType)
		{
			SetName(name);
			SetImageSource(partialResource, resolvingType);

			return this;
		}

		public LiquidBaseBuilder<TLiquidBase> SetName(string name)
		{
			Name = name;
			return this;
		}

		public LiquidBaseBuilder<TLiquidBase> SetImageSource(string partialResource, Type resolvingType)
		{
			if (resolvingType == null) throw ExceptionFactory.Get<ArgumentNullException>("'resolvingType' is null");
			if (string.IsNullOrWhiteSpace(partialResource))
				throw ExceptionFactory.Get<ArgumentException>("'partialResource' is null or has whitespaces");

			var resource = string.Join(".", "SmartButler.Resources", partialResource);
			var sourceAssembly = resolvingType.GetTypeInfo().Assembly;

			byte[] byteImage;
			using (var stream = sourceAssembly.GetManifestResourceStream(resource))
			{
				if (stream == null)
				{
					ActualImage = null;
					ByteImage = null;
					return this;
				}
				var length = stream.Length;
				byteImage = new byte[length];
				stream.Read(byteImage, 0, (int)length);
				ActualImage = ImageSource.FromStream(() => new MemoryStream(byteImage));
			}

			ByteImage = byteImage;

			return this;
		}
	}
}
