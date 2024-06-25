using System;
using System.Net;

namespace Yandex.API360.Exceptions {
    public class APIRequestException : Exception {
        public HttpStatusCode? HttpStatusCode { get; set; }
        public FailedAPIResponse ErorrData { get; set; }
        public APIRequestException(HttpStatusCode httpStatusCode, FailedAPIResponse failedAPIResponse) : base(failedAPIResponse.ToString()) {
            HttpStatusCode = httpStatusCode;
            ErorrData = failedAPIResponse;
        }
        public APIRequestException(string message, HttpStatusCode httpStatusCode) : base(message) {
            HttpStatusCode = httpStatusCode;
        }
    }
}
