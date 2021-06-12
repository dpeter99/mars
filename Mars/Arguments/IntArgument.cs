using System;
using System.Globalization;

namespace Mars.Arguments
{
    public class IntArgument : ArgumentType<int>
    {
        public override int Parse(StringReader text, ParseResult parseResult)
        {
            var token = text.GetNextToken();

            if (int.TryParse(token, out var result))
            {
                text.PopNextToken();
                return result;
            }
            else
            {
                throw new ArgumentException("The int couldn't be parsed", nameof(text));
            }
        }

        public override bool CanParse(string text)
        {
            var tokens = text.Split(' ', 1);
            
            return int.TryParse(tokens[0],out _);
            
        }
    }
}