using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.Common
{
    public enum ApplicationErrorCode
    {
        None = 0,
        UnauthorizedAction = 7,
        NotFound = 8,
        BadRequest = 9,
        InvalidInput = 10,
    }
}
