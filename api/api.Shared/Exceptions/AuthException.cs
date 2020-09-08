using System;

namespace api.Shared.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {
        }
    }
}