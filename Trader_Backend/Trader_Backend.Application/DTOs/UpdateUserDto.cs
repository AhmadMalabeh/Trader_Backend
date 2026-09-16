using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.DTOs
{
    public class UpdateUserDto
    {
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
    }
}
