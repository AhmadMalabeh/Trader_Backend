using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.DTOs
{
    public class OptionGroupDto
    {
        public string Value { get; set; } = null!;
        public int CategoryID { get; set; }
    }
}
