using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class OptionGroup
    {
        public int ID { get; set; }
        public string Value { get; set; } = null!;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
