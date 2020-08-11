using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Class for validation errors steming from data sent from requests, used to differentiate failed requests (HTTP 400) from a 500 Internal Server Error
    /// </summary>
    public class RequestErrorException : Exception
    {
        public RequestErrorException(): base() { }
        public RequestErrorException(string message): base(message) { }
    }
}
