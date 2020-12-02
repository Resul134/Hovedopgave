using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class FilterTask
    {
        public int CategoryId { get; set; }
        public string Region { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public double PriceLow { get; set; }
        public double PriceHigh { get; set; }

        public FilterTask()
        {

        }

        public override string ToString()
        {
            return $"{nameof(CategoryId)}: {CategoryId}, {nameof(Region)}: {Region}, {nameof(DateStart)}: {DateStart}, {nameof(DateEnd)}: {DateEnd}, {nameof(PriceLow)}: {PriceLow}, {nameof(PriceHigh)}: {PriceHigh}";
        }
    }
}
