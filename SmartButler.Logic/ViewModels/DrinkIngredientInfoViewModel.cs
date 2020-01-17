using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Logic.Common;

namespace SmartButler.Logic.ViewModels
{
	public class DrinkIngredientInfoViewModel : DrinkIngredientBaseViewModel
	{
		private string _infoText;

		public DrinkIngredientInfoViewModel()
		{
			
		}

		public string InfoText
		{
			get => _infoText;
			set => SetValue(ref _infoText, value);
		}
	}

	public abstract class DrinkIngredientBaseViewModel : BaseViewModel
	{

	}
}
