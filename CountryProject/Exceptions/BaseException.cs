using System;
using System.Net;

namespace CountryProject.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.BadRequest;
        public BaseException(string message) : base(message) {}
    }

    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) {
            HttpStatusCode = HttpStatusCode.NotFound;
        }
    }

    public class NotSuccessfulException : BaseException
    {
        public NotSuccessfulException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
        }
    }

}
