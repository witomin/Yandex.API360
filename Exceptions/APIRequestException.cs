using System;
using System.Net;

namespace Yandex.API360.Exceptions {
    public class APIRequestException : Exception {
        public HttpStatusCode? HttpStatusCode { get; }
        public int Code { get; }
        public object[] Details { get; }
        public APIRequestException(HttpStatusCode httpStatusCode, string message, int code, object[] details) : base(message) {
            HttpStatusCode = httpStatusCode;
            Code = code;
            Details = details;
        }
        public APIRequestException(string message, HttpStatusCode httpStatusCode) : base(message) {
            HttpStatusCode = httpStatusCode;
        }
    }
}
