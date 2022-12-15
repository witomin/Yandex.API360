using System;
using System.Collections.Generic;
using System.Net;

namespace Yandex.API360.Exceptions {
    public class APIRequestException : Exception {
        public HttpStatusCode? HttpStatusCode { get; }
        public int Code { get; }
        public List<string> Details { get; }
        public APIRequestException(HttpStatusCode httpStatusCode, string message, int code, List<string> details) : base(message) {
            HttpStatusCode = httpStatusCode;
            Code = code;
            Details = details;
        }
        public APIRequestException(HttpStatusCode httpStatusCode, FailedAPIResponse failedAPIResponse) : base(failedAPIResponse.message) {
            HttpStatusCode = httpStatusCode;
            Code = failedAPIResponse.code;
            Details = failedAPIResponse.details;
        }
        public APIRequestException(string message, HttpStatusCode httpStatusCode) : base(message) {
            HttpStatusCode = httpStatusCode;
        }
    }
}
