using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class AdImage
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int AdID { get; set; }
        public Ad Ad { get; set; } = null!;
    }
}
