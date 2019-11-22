using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartButler.Logic.Interfaces
{
	public interface IUserInteraction
	{
		Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);
		Task DisplayAlertAsync(string title, string message, string cancel);
		Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
	}
}
