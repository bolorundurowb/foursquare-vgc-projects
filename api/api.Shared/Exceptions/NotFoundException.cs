using System;

namespace api.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}