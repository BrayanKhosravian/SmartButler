using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BottlesViewCell : ViewCell
	{
		public BottlesViewCell ()
		{
			InitializeComponent ();
		}
	}
}