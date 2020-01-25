using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartButler.View.Common
{
	public static class AnimationService
	{
		public static async Task ReFadeAsync(VisualElement visualElement)
		{
			await visualElement.FadeTo(0.5, 200, Easing.SinIn);
			await visualElement.FadeTo(1, 450, Easing.SinIn);
		}

		public static async Task MakeVisibleAsync(VisualElement visualElement)
		{
			await visualElement.FadeTo(0, 0, Easing.SinIn);
			await visualElement.FadeTo(1, 450, Easing.SinIn);
		}
	}
}
