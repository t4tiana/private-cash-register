using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Wrapped { get; set; }
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ShortProductType { get; set; }

        public string LongProductType
        {
            get
            {
                switch(ShortProductType)
                {
                    case "CH":
                        return "Chocolate Confectionary";
                        break;
                    case "SR":
                        return "Sour Flavored Candies";
                    case "HC":
                        return "Hard Tack Confectionary";
                    case "LI":
                        return "Licorice and Jellies";
                    default:
                        return "Other";
                        break;
                }
            }
        }
        private string IsItSoldOut()
        {
            if(Quantity == 0)
            {
                return "SOLD OUT";
            }
            return Quantity.ToString();
        }
        public override string ToString()
        {
            string eachLine = Id.PadRight(10) + Name.PadRight(30) + Wrapped.PadRight(10) +
                 IsItSoldOut().PadRight(10) + Price.ToString().PadRight(5);

            return eachLine;
        }
    }
}
