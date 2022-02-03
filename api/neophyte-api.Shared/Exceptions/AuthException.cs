using System;

namespace neophyte.api.Shared.Exceptions;

public class AuthException : Exception
{
    public AuthException(string message) : base(message)
    {
    }
}