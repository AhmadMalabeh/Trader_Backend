using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
        public ICollection<OptionGroup> OptionGroups { get; set; } = new List<OptionGroup>();
    }
}
