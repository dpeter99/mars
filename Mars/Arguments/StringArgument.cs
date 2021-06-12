namespace Mars.Arguments
{
    public class StringArgument : ArgumentType<string>
    {
        public override string Parse(StringReader text, ParseResult parseResult)
        {
            var token = text.PopNextToken();
            
            return token;
        }

        public override bool CanParse(string text)
        {
            return text.Length > 0;
        }
    }
}