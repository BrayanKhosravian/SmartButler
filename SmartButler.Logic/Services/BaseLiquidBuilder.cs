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

		public virtual TBuilder Default(string name, string partialResource, Type resolvingType)
		{
			SetName(name);
			SetByteImage(partialResource, resolvingType);

			return BuilderInstance;
		}

		public TBuilder SetName(string name)
		{
			Name = name;
			return BuilderInstance;
		}

		public TBuilder SetByteImage(string partialResource, Type resolvingType)
		{ 
			if (resolvingType == null) throw ExceptionFactory.Get<ArgumentNullException>("'resolvingType' is null");
			if (string.IsNullOrWhiteSpace(partialResource))
				throw ExceptionFactory.Get<ArgumentException>("'partialResource' is null or has whitespaces");

			var ass = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "SmartButler");

			var resource = string.Join(".", "SmartButler.Resources", partialResource);
			var sourceAssembly = resolvingType.GetTypeInfo().Assembly;

			byte[] byteImage;
			using (var stream = ass?.GetManifestResourceStream(resource))
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
