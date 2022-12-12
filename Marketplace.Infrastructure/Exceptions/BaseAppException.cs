using System;
using System.Net;

namespace Marketplace.Infrastructure.Exceptions
{
    /// <summary>
    /// Base custom exception.
    /// </summary>
    public abstract class BaseAppException : Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }
        public ErrorDataObject ErrorData { get; set; }

        public BaseAppException(ErrorDataObject errorData) : base()
        {
            ErrorData = errorData;
        }

        public BaseAppException(string message, ErrorDataObject errorData) : base(message)
        {
            ErrorData = errorData;
        }

        public BaseAppException(string message, Exception innerException, ErrorDataObject errorData) : base(message, innerException)
        {
            ErrorData = errorData;
        }
    }
}
