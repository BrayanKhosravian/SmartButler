using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartButler.Framework.Extensions
{
	public static class TaskExtensions
	{
		public static async Task AwaitSafe(this Task task, bool continueOnCapturedContext = true)
		{
			try
			{
				await task.ConfigureAwait(continueOnCapturedContext);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}   
}

