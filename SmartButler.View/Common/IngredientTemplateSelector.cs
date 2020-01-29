using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Framework.Common;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Cells;
using Xamarin.Forms;

namespace SmartButler.View.Common
{
	class IngredientTemplateSelector : DataTemplateSelector
	{
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			switch (item)
			{
				case DrinkIngredientViewModelInfo _:
					return DrinkIngredientInfoTemplate;
				case DrinkIngredientViewModel _:
					return DrinkIngredientTemplate;
				default:
					throw ExceptionFactory.Get<NotImplementedException>("DataTemplate not implemented!");
			}
		}

		public DataTemplate DrinkIngredientTemplate => new DataTemplate(typeof(IngredientViewCell));
		public DataTemplate DrinkIngredientInfoTemplate => new DataTemplate(typeof(IngredientInfoViewCell));
	}
}
