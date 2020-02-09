using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[ContentProperty(nameof(MyContent))]
	public partial class RoundView : Frame
	{
		private Xamarin.Forms.View _myContent;

		public RoundView()
		{
			InitializeComponent();
		}

		public Xamarin.Forms.View MyContent
		{
			get => _myContent;
			set
			{
				_myContent = value;
				base.Content = value;
			}
		}


		[TypeConverter(typeof(ColorTypeConverter))]
		public Color RoundBackGroundColor
		{
			get => (Color) GetValue(RoundBackGroundColorProperty);
			set
			{
				if(RoundBackGroundColor != value)
					SetValue(RoundBackGroundColorProperty, value);
			}
		}

		public static readonly BindableProperty RoundBackGroundColorProperty =
			BindableProperty.Create(nameof(RoundBackGroundColor), 
				typeof(Color),
				typeof(RoundView),
				Color.Default,
				BindingMode.TwoWay);

		public float RoundCornerRadius
		{
			get => (float) GetValue(RoundCornerRadiusProperty);
			set => SetValue(RoundCornerRadiusProperty, value);
		}

		public static readonly BindableProperty RoundCornerRadiusProperty =
			BindableProperty.Create(nameof(RoundCornerRadius),
				typeof(float),
				typeof(RoundView),
				20f,
				BindingMode.OneTime);


		[TypeConverter(typeof(ThicknessTypeConverter))]
		public Thickness RoundMargin
		{
			get => (Thickness) GetValue(RoundMarginProperty);
			set
			{
				if(RoundMargin == value) return;
				SetValue(RoundMarginProperty, value);
			}
		}

		public static readonly BindableProperty RoundMarginProperty =
			BindableProperty.Create(nameof(RoundMargin), 
				typeof(Thickness),
				typeof(RoundView),
				new Thickness(10,5),
				BindingMode.OneTime);



		[TypeConverter(typeof(ThicknessTypeConverter))]
		public Thickness RoundPadding
		{
			get => (Thickness) GetValue(RoundPaddingProperty);
			set
			{
				if(RoundMargin == value) return;
				SetValue(RoundPaddingProperty, value);
			}
		}

		public static readonly BindableProperty RoundPaddingProperty =
			BindableProperty.Create(nameof(RoundPadding), 
				typeof(Thickness),
				typeof(RoundView),
				new Thickness(10),
				BindingMode.OneTime);

	}
}