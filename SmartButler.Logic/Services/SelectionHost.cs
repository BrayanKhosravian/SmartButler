using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.Services
{
	public interface ISelectionHost<T> where T : class
	{
		bool IsAutoReset { get; set; }
		bool IsAvailable { get; }
		T Selection { get; set; }
		void Reset();
	}

	public abstract class SelectionHostBase<T> : ISelectionHost<T> where T : class
	{
		private T _selection;
		public abstract bool IsAutoReset { get; set; }
		public bool IsAvailable => _selection != null;

		public virtual T Selection
		{
			get
			{
				if (!IsAutoReset)
					return _selection;

				var tmp = _selection;
				_selection = null;
				return tmp;
			}
			set => _selection = value;
		}

		public virtual void Reset() => Selection = null;
	}

	public sealed class DrinkIngredientSelectionHost : SelectionHostBase<DrinkIngredientViewModel>
	{
		public override bool IsAutoReset { get; set; } = true;
	}

	public sealed class IngredientSelectionHost : SelectionHostBase<DrinkIngredientViewModel>
	{
		public override bool IsAutoReset { get; set; } = true;
	}
}
