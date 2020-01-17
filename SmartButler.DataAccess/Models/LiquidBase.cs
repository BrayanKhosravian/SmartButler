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

	    public virtual string Name { get; set; }

	    public virtual byte[] ByteImage { get; set; }

	    public virtual bool IsDefault { get; set; }

	}
}
