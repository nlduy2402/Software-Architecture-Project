using GrabServerCore.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerCore.DTOs
{
    public class ResponseDetails
    {
        public ResponseDetails(ResponseStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ResponseStatusCode StatusCode { get; protected set; } = ResponseStatusCode.Ok;
        public string Message { get; protected set; }
    }
}
