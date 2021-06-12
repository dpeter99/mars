namespace Mars
{
    public class LiteralNode : CommandNode
    {
        public LiteralNode(string txt, CommandMetadata meta = null): base(txt, meta)
        {
            
        }

        public override bool CanParse(string text)
        {
            var index = text.IndexOf(' ');
            if (index > 0)
            {
                var token = text.Substring(0, index);
                return token.ToLowerInvariant() == _name;
            }
            else
            {
                return text == _name;
            }
        }
        
        
        public override bool ParseSingleToken(string token, ParseResult parseResult)
        {
            if (token == _name)
            {
                parseResult.Nodes.Add(this);
                return true;
            }

            return false;
        }

        public override void Parse(StringReader text, ParseResult parseResult)
        {
            var token = text.GetNextToken();

            if (token == _name)
            {
                text.PopNextToken();
            }

            base.Parse(text, parseResult);

        }

        public override string GetUsageText()
        {
            return _name;
        }

        public override string ToString()
        {
            return "Literal: " + _name;
        }
    }
}