using System;

namespace CIEDigitalLib.Exceptions
{
    [Serializable]
    public class InvalidSearchException : Exception
    {
        public InvalidSearchException(string message)
            : base(message)
        {
        }
    }
}