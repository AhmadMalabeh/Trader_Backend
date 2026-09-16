using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class Option
    {
        public int ID { get; set; }
        public string Value { get; set; } = null!;
        public int OptionGroupID { get; set; }
        public OptionGroup OptionGroup { get; set; } = null!;
    }
}
