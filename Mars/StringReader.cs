namespace Mars
{
    public class StringReader
    {
        private readonly string _fulltext;

        private string _remaining;


        public StringReader(string s)
        {
            _fulltext = s;
            _remaining = _fulltext;
        }

        public override string ToString()
        {
            return _remaining;
        }

        public static implicit operator string(StringReader d)
        {
            return d._remaining;
        }


        public string GetText()
        {
            return _remaining;
        }
        

        public string GetNextToken()
        {
            var tokens = _remaining.Split(' ', 2);
            
            return tokens[0];
        }
        
        public string PopNextToken()
        {
            var tokens = _remaining.Split(' ', 2);
            _remaining = _remaining.Remove(0, tokens[0].Length).TrimStart();
            
            return tokens[0];
        }

        public string GetFullText()
        {
            return _fulltext;
        }
        
        public string PopFullText()
        {
            var token = _remaining;
            _remaining = "";
            return token;
        }
    }
}