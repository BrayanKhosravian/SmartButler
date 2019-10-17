using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace SmartButler.Models
{
    public class Drink : LiquidContainer
    {
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class Ingredient
    {
        public int BottleNumber { get; set; }
        public string BottleName { get; set; }
        public int Milliliter { get; set; }
    }
}
