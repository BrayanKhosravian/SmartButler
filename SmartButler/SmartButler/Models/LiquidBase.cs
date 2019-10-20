using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace SmartButler.Models
{
    public abstract class LiquidBase
    {
	    public string Name { get; set; }

	    public byte[] ByteImage { get; set; }

	    [Ignore]
	    public ImageSource ActualImage { get; set; }

		protected LiquidBase()
	    {
		    
	    }

	    protected LiquidBase(string name)
	    {
		    Name = name;
	    }

	   
    }
}
