using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JudgeServerInterface
{
    class InvalidRequest : Exception
    {
        public InvalidRequest(String message) : base(message) { }
    }

    class InvalidUrl : Exception
    {
        public InvalidUrl(String message) : base(message) { }
    }

    class InternalServerError : Exception
    {
        public InternalServerError(String message) : base(message) { }
    }

    class UnknownError : Exception
    {
        public UnknownError(String message) : base(message) { }
    }
}
