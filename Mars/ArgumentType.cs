namespace Mars
{
    public abstract class ArgumentType<T>
    {
        public abstract T Parse(StringReader text, ParseResult parseResult);

        public abstract bool CanParse(string text);
    }
}