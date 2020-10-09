using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
            : base("Unable to authenticate") { }
    }
}
