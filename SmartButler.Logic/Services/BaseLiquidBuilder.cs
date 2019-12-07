using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;

namespace SmartButler.Logic.Services
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

		public virtual TBuilder Default(string name, string resourcePath)
		{
			SetName(name);
			SetByteImage(resourcePath);

			return BuilderInstance;
		}

		public TBuilder SetName(string name)
		{
			Name = name;
			return BuilderInstance;
		}

		public TBuilder SetByteImage(string resourcePath)
		{
			if (string.IsNullOrWhiteSpace(resourcePath))
				throw ExceptionFactory.Get<ArgumentException>("'resourcePath' is null or has whitespaces");

			var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "SmartButler.Framework");

			byte[] byteImage;
			using (var stream = assembly?.GetManifestResourceStream(resourcePath))
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
