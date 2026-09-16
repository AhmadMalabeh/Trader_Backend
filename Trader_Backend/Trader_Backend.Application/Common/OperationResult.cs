using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public ApplicationErrorCode ErrorCode { get; private set; } // 👈 تحول إلى enum بشكل نظيف

        private OperationResult(T data)
        {
            IsSuccess = true;
            Data = data;
            ErrorCode = ApplicationErrorCode.None;
            ErrorMessage = null;
        }

        private OperationResult(ApplicationErrorCode errorCode, string errorMessage)
        {
            IsSuccess = false;
            Data = default;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static OperationResult<T> Success(T data) => new OperationResult<T>(data);

        public static OperationResult<T> Failure(ApplicationErrorCode errorCode, string errorMessage)
            => new OperationResult<T>(errorCode, errorMessage);
    }
}
