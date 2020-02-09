using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartButler.View.Common
{
	public static class AnimationService
	{
		public static async Task VisualElementClicked(VisualElement visualElement)
		{

			var animateInto = new Func<List<Task>>(() => new List<Task>()
			{
			 	visualElement.FadeTo(0.5, 300, Easing.SinIn),
			 	visualElement.ScaleTo(0.95, 300, Easing.SinIn)
			});

			var animateOut = new Func<List<Task>>(() => new List<Task>()
			{
			 	visualElement.FadeTo(1, 450, Easing.SinIn),
			 	visualElement.ScaleTo(1, 450, Easing.SinIn)
			});

			await Task.WhenAll(animateInto.Invoke());
			await Task.WhenAll(animateOut.Invoke());
		}

		public static async Task MakeVisibleAsync(VisualElement visualElement)
		{
			await visualElement.FadeTo(0, 0, Easing.SinIn);
			await visualElement.FadeTo(1, 450, Easing.SinIn);
		}
	}
}
