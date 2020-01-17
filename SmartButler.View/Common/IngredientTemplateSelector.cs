using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Framework.Common;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;

namespace SmartButler.View.Common
{
	class IngredientTemplateSelector : DataTemplateSelector
	{
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (item is DrinkIngredientInfoViewModel)
				return DrinkIngredientInfoTemplate;
			else if (item is DrinkIngredientViewModel)
				return DrinkIngredientTemplate;
			else throw ExceptionFactory.Get<NotImplementedException>("Datatemplate not implemented!");
		}

		public DataTemplate DrinkIngredientTemplate { get; set; }
		public DataTemplate DrinkIngredientInfoTemplate { get; set; }
	}
}
