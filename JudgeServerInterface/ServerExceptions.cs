using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JudgeServerInterface
{
    public class InvalidRequest : Exception
    {
        public InvalidRequest(String message) : base(message) { }
    }

    public class InvalidUrl : Exception
    {
        public InvalidUrl(String message) : base(message) { }
    }

    public class InternalServerError : Exception
    {
        public InternalServerError(String message) : base(message) { }
    }

    public class UnknownError : Exception
    {
        public UnknownError(String message) : base(message) { }
    }
}
