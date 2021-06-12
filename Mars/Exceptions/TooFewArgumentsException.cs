using System;

namespace Mars.Exceptions
{
    public class TooFewArgumentsException : Exception
    {
        public override string ToString()
        {
            return "The command needs more arguments";
        }
    }
}