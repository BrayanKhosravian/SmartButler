namespace SmartButler.Logic.ModelViewModels
{
	public class DrinkIngredientViewModelInfo : DrinkIngredientViewModelBase
	{
		private string _infoText;

		public DrinkIngredientViewModelInfo()
		{
			
		}

		public string InfoText
		{
			get => _infoText;
			set => SetValue(ref _infoText, value);
		}

		
	}
}
