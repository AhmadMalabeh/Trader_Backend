using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.DTOs
{
    public class RegisterRequestDto
    {

        public string? InvitationCode { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
