using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.DTOs
{
    public class OptionGroupViewDto
    {
        public int ID { get; set; }
        public string Value { get; set; } = null!;
        public int CategoryID { get; set; }
    }
}
