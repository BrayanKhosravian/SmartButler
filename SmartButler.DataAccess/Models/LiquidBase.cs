using System.IO;
using SQLite;

namespace SmartButler.DataAccess.Models
{
    public abstract class LiquidBase
    {
	    protected LiquidBase() { }

	    protected LiquidBase(string name)
	    {
		    Name = name;
	    }

	    public string Name { get; set; }

	    public byte[] ByteImage { get; set; }

	}
}
