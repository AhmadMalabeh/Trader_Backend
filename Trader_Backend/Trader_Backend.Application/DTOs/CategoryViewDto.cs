using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.DTOs
{
    public class CategoryViewDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
