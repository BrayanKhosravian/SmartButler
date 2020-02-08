using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoundImage : ContentView
	{
		public RoundImage()
		{
			InitializeComponent();
		}

		public Thickness RoundImagePadding
		{
			get => (Thickness)GetValue(RoundImagePaddingProperty);
			set
			{
				if (this.RoundImagePadding != value)
					SetValue(RoundImagePaddingProperty, value);
			}
		}

		public static readonly BindableProperty RoundImagePaddingProperty =
			BindableProperty.Create(nameof(RoundImagePadding),
				typeof(Thickness),
				typeof(RoundImage),
				defaultValue: new Thickness(2),
				BindingMode.OneTime);



		public ImageSource RoundImageSource
		{
			get => (ImageSource)GetValue(RoundImageSourceProperty);
			set
			{
				if (this.RoundImageSource != value)
					SetValue(RoundImageSourceProperty, value);
			}
		}

		public static readonly BindableProperty RoundImageSourceProperty =
			BindableProperty.Create(nameof(RoundImageSource),
				typeof(ImageSource),
				typeof(RoundImage),
				defaultValue: ImageSource.FromStream(() => new MemoryStream(new byte[0])),
				BindingMode.OneWay);



		public float RoundImageCornerRadius
		{
			get => (float)GetValue(RoundImageCornerRadiusProperty);
			set => SetValue(RoundImageCornerRadiusProperty, value);
		}

		public static readonly BindableProperty RoundImageCornerRadiusProperty =
			BindableProperty.Create(nameof(RoundImageCornerRadius),
				typeof(float),
				typeof(RoundImage),
				15f,
				BindingMode.OneTime,
				(bindable, value) =>
				{
					if (float.TryParse(value.ToString(), out var result) && result >= 0)
						return true;
					else return false;
				});



		[TypeConverter(typeof(ColorTypeConverter))]
		public Color RoundImageBorderColor
		{
			get => (Color)GetValue(RoundImageBorderColorProperty);
			set
			{
				if(this.RoundImageBorderColor != value)
					SetValue(RoundImageBorderColorProperty, value);
			}
		}

		public static readonly BindableProperty RoundImageBorderColorProperty =
			BindableProperty.Create(nameof(RoundImageBorderColor),
				typeof(Color),
				typeof(RoundImage),
				Color.Default,
				BindingMode.OneTime);





		public double RoundImageHeight
		{
			get => (double)GetValue(RoundImageHeightProperty);
			set => SetValue(RoundImageHeightProperty, value);
		}

		public static readonly BindableProperty RoundImageHeightProperty =
			BindableProperty.Create(nameof(RoundImageHeightProperty),
				typeof(double),
				typeof(RoundImage),
				75d,
				BindingMode.OneTime, 
				(bindable, value) =>
				{
					var converted = double.TryParse(value.ToString(), out var result);
					if (converted && result >= 0)
						return true;

					return false;
				});




		public double RoundImageWidth
		{
			get => (double)GetValue(RoundImageWidthProperty);
			set => SetValue(RoundImageWidthProperty, value);
		}

		public static readonly BindableProperty RoundImageWidthProperty =
			BindableProperty.Create(nameof(RoundImageHeightProperty),
				typeof(double),
				typeof(RoundImage),
				75d,
				BindingMode.OneTime, 
				(bindable, value) =>
				{
					var converted = double.TryParse(value.ToString(), out var result);
					if (converted && result >= 0)
						return true;

					return false;
				});
		
	}
}