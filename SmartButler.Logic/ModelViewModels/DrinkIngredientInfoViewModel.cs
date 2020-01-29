namespace SmartButler.Logic.ModelViewModels
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
}
