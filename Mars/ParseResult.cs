using System;
using System.Collections.Generic;

namespace Mars
{
    public class ParseResult
    {
        public Dictionary<string, object> Args = new();

        public List<CommandNode> Nodes = new();

        public Exception error;
        
        public CommandCallback Callback;


        public string GetStringArg(string id)
        {
            return (string) Args[id];
        }
        
        public int GetIntArg(string id)
        {
            return (int) Args[id];
        }
    }
}