using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.Common
{
    public enum ApplicationErrorCode
    {
        None = 0,

        // أخطاء المستخدمين
        UserNotFound = 1,
        EmailAlreadyExists = 2,
        InvalidPhoneNumber = 3,

        // أخطاء أكواد الدعوة
        InvitationCodeNotFound = 4,
        InvitationCodeAlreadyUsed = 5,

        // أخطاء الإعلانات
        AdNotFound = 6,
        UnauthorizedAction = 7
    }
}
