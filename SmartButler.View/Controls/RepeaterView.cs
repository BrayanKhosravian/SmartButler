﻿using System.Collections;
using Xamarin.Forms;

namespace SmartButler.View.Controls
{
	public class RepeaterView : StackLayout
	{
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
			nameof(ItemTemplate),
			typeof(DataTemplate),
			typeof(RepeaterView),
			default(DataTemplate));

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
			nameof(ItemsSource),
			typeof(ICollection),
			typeof(RepeaterView),
			null,
			BindingMode.OneWay,
			propertyChanged: ItemsChanged);

		public RepeaterView()
		{
			Spacing = 0;
		}

		public ICollection ItemsSource
		{
			get => (ICollection)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public DataTemplate ItemTemplate
		{
			get => (DataTemplate)GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
		}

		protected virtual Xamarin.Forms.View ViewFor(object item)
		{
			Xamarin.Forms.View view = null;

			if (ItemTemplate != null)
			{
				var content = ItemTemplate.CreateContent();

				view = content is Xamarin.Forms.View ? content as Xamarin.Forms.View : ((ViewCell)content).View;

				view.BindingContext = item;
			}

			return view;
		}

		private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = bindable as RepeaterView;

			if (control == null) return;

			control.Children.Clear();

			var items = (ICollection)newValue;

			if (items == null) return;

			foreach (var item in items)
			{
				control.Children.Add(control.ViewFor(item));
			}
		}
	}
}
