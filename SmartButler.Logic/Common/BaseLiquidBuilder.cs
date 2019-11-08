using System;
using System.Linq;
using System.Reflection;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;

namespace SmartButler.Logic.Common
{
	public abstract class BaseLiquidBuilder<TLiquidBase, TBuilder> 
		where TLiquidBase : LiquidBase
		where TBuilder : BaseLiquidBuilder<TLiquidBase, TBuilder>
	{
		protected string Name;
		protected byte[] ByteImage;

		public abstract TLiquidBase Build();

		protected abstract TBuilder BuilderInstance { get; }

		public virtual TBuilder Default()
		{
			Name = null;
			ByteImage = null;

			return BuilderInstance;
		}

		public virtual TBuilder Default(string name, string partialResource)
		{
			SetName(name);
			SetByteImage(partialResource);

			return BuilderInstance;
		}

		public TBuilder SetName(string name)
		{
			Name = name;
			return BuilderInstance;
		}

		public TBuilder SetByteImage(string partialResource)
		{
			if (string.IsNullOrWhiteSpace(partialResource))
				throw ExceptionFactory.Get<ArgumentException>("'partialResource' is null or has whitespaces");

			var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "SmartButler");
			var resource = string.Join(".", "SmartButler.Resources", partialResource);

			byte[] byteImage;
			using (var stream = assembly?.GetManifestResourceStream(resource))
			{
				if (stream == null)
				{
					ByteImage = null;
					return BuilderInstance;
				}
				var length = stream.Length;
				byteImage = new byte[length];
				stream.Read(byteImage, 0, (int)length);
			}

			ByteImage = byteImage;

			return BuilderInstance;
		}
	}
}
