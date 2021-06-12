namespace Mars
{
    public class ArgumentNode<T> : CommandNode, IArgumentNode
    {
        private readonly ArgumentType<T> _type;

        public ArgumentNode(string name,CommandMetadata meta, ArgumentType<T> type) : base(name, meta)
        {
            _type = type;
        }

        public override bool CanParse(string text)
        {
            var ret = _type.CanParse(text);

            return ret;
        }

        public override bool ParseSingleToken(string token, ParseResult parseResult)
        {
            if(_type.CanParse(token))
            {
                var res = _type.Parse(new StringReader(token), parseResult);
                parseResult.Args.Add(_name,res);
                return true;
            }

            return false;
        }
        
        public override void Parse(StringReader text, ParseResult parseResult)
        {
            var res = _type.Parse(text, parseResult);
            parseResult.Args.Add(_name,res);
            
            base.Parse(text,parseResult);
        }

        public override string GetUsageText()
        {
            return $"<{_name}>";
        }

        public override string ToString()
        {
            return $"Argument: {_name} type: {_type.GetType()}";
        }
    }

    public interface IArgumentNode
    {
    }
}