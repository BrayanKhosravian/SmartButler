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
	//public interface IBaseLiquidBuilder<TLiquidBase> where TLiquidBase : LiquidBase
	//{
	//	BaseLiquidBuilder<TLiquidBase> Default();

	//	BaseLiquidBuilder<TLiquidBase> Default(string name, string partialResource, Type resolvingType);

	//	BaseLiquidBuilder<TLiquidBase> SetName(string name);
	//	BaseLiquidBuilder<TLiquidBase> SetImageSource(string partialResource, Type resolvingType);

	//	TLiquidBase Build();

	//}

	public abstract class BaseLiquidBuilder<TLiquidBase, TBuilder> 
		where TLiquidBase : LiquidBase
		where TBuilder : BaseLiquidBuilder<TLiquidBase, TBuilder>
	{
		protected string Name;
		protected byte[] ByteImage;
		protected ImageSource ActualImage;

		public abstract TLiquidBase Build();

		protected abstract TBuilder BuilderInstance { get; }

		public virtual TBuilder Default()
		{
			Name = null;
			ByteImage = null;
			ActualImage = null;

			return BuilderInstance;
		}

		public virtual TBuilder Default(string name, string partialResource, Type resolvingType)
		{
			SetName(name);
			SetImageSource(partialResource, resolvingType);

			return BuilderInstance;
		}

		public TBuilder SetName(string name)
		{
			Name = name;
			return BuilderInstance;
		}

		public TBuilder SetImageSource(string partialResource, Type resolvingType)
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
					return BuilderInstance;
				}
				var length = stream.Length;
				byteImage = new byte[length];
				stream.Read(byteImage, 0, (int)length);
				ActualImage = ImageSource.FromStream(() => new MemoryStream(byteImage));
			}

			ByteImage = byteImage;

			return BuilderInstance;
		}
	}
}
