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

		//private ImageSource _imageSource;
		//[Ignore]
		//public ImageSource ActualImage
		//{
		//	get
		//	{
		//		if (_imageSource == null && ByteImage != null)
		//			return ImageSource.FromStream(() => new MemoryStream(ByteImage));

		//		return _imageSource;
		//	}
		//	set => _imageSource = value;
		//}


	}
}
