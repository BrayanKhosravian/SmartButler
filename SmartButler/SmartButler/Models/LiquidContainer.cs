using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartButler.Models
{
    public abstract class LiquidContainer
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public string Name { get; set; }
        //public byte[] ImageData { get; set; }
        public ImageSource ActualImage { get; set; }
    }
}
