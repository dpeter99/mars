namespace Mars.Arguments
{
    public class LongStringArgument: ArgumentType<string>
    {
        public override string Parse(StringReader text, ParseResult parseResult)
        {
            return text.PopFullText();
        }

        public override bool CanParse(string text)
        {
            return text.Length > 0;
        }
    }
}