using System;
using System.Collections.Generic;
using System.Text;

namespace SmartButler.Framework.Extensions
{
	public static class InputValidationExtensions
	{

		public static bool IsInputValid(this string input, int minLength = 5, int maxLength = 250)
		{
			if (!string.IsNullOrWhiteSpace(input) && input.Length >= minLength && input.Length <= maxLength)
				return true;

			return false;
		}

		public static bool IsInputValid(this int input, int min = 30, int max = 500)
		{
			if (input >= min && input <= max)
				return true;

			return false;
		}
	}
}
