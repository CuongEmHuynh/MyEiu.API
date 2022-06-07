using System;
using System.Collections.Generic;
using System.Text;

namespace MyEiu.Utilities.Dtos
{
    public class OperationResult
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }
        public bool Success { set; get; }
        public object Data { set; get; }

    }
}
