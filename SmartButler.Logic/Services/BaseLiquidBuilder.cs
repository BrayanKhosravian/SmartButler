using System;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;

namespace SmartButler.Logic.Services
{
	public abstract class BaseLiquidBuilder<TLiquidBase, TBuilder> 
		where TLiquidBase : LiquidBase
		where TBuilder : BaseLiquidBuilder<TLiquidBase, TBuilder>
	{

		protected string Name;
		protected byte[] ByteImage;
		private bool _isDefault;

		public abstract TLiquidBase Build();

		protected abstract TBuilder BuilderInstance { get; }

		public virtual TBuilder TakeDefault(LiquidBase liquidBase)
		{
			Name = liquidBase.Name;
			ByteImage = liquidBase.ByteImage;
			_isDefault = liquidBase.IsDefault;

			return BuilderInstance;
		}

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

			var byteImage = ResourceManager.GetImageAsBytes(resourcePath);
			ByteImage = byteImage;

			return BuilderInstance;
		}
	}
}
