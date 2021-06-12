using System;

namespace Mars
{
    public class CommandTree
    {
        private CommandNode root = new ("root");
        
        public void AddNode(CommandNode node)
        {
            root.AddChild(node);
        }

        public ParseResult ParseString(string text)
        {
            var res = new ParseResult();

            var reader = new StringReader(text);
            try
            {
                root.Parse(reader, res);
            }
            catch (Exception e)
            {
                res.error = e;
                //throw;
            }
            
            
            return res;
        }

        public CommandNode GetRoot()
        {
            return root;
        }
    }
}