using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingData.ModelLayer
{
    public class Price
    {
        public int Id { get; set; }
        public double? NormalPrice { get; set; }
        public double? SpecialPrice { get; set; }
        public string? Weekday { get; set; }

        // Empty constructor
        public Price() { }

        // Constructor with parameters
        public Price(double? normalPrice, double? specialPrice, string? weekday)
        {
            NormalPrice = normalPrice;
            SpecialPrice = specialPrice;
            Weekday = weekday;
        }
        //Reuses constructor with id parameter
        public Price(int id, double? normalPrice, double? specialPrice, string? weekday) : this(normalPrice, specialPrice, weekday)
        {
            Id = id;
        }

    }
}
