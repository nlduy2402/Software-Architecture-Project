using GrabServerCore.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerCore.DTOs
{
    public class ResponseMessageDetails<TData> : ResponseDetails
    {
        public ResponseMessageDetails(string message, ResponseStatusCode statusCode = ResponseStatusCode.Ok) : base(statusCode, message)
        {
            Data = default;
        }

        public ResponseMessageDetails(string message, TData data, ResponseStatusCode statusCode = ResponseStatusCode.Ok) : base(statusCode, message)
        {
            Data = data;
        }

        public TData Data { get; set; }
    }
}
