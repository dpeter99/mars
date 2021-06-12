using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mars.Exceptions;

namespace Mars
{
    public class CommandNode
    {
        public CommandMetadata? Meta { get; }
        
        protected string _name;
        public string Name => _name;
        
        
        protected List<CommandNode> _children = new();
        
        public IEnumerable<CommandNode> Children => _children.AsReadOnly();

        public bool EndNode;
        public CommandCallback Callback { get; protected internal set; }
        
        
        public CommandNode(string name, CommandMetadata meta = null)
        {
            Meta = meta;
            _name = name;
        }

        

        public void AddChild(CommandNode commandNode)
        {
            _children.Add(commandNode);
        }


        public virtual bool CanParse(string text)
        {
            return true;
        }

        /// <summary>
        /// Try's to parse the given token, if successful as the data to the parse result
        /// </summary>
        /// <param name="token">The token to parse</param>
        /// <param name="parseResult">The result for collecting the arguments</param>
        /// <returns></returns>
        public virtual bool ParseSingleToken(string token, ParseResult parseResult)
        {
            parseResult.Nodes.Add(this);
            return true;
        }
        
        public virtual void Parse(StringReader text, ParseResult parseResult)
        {
            parseResult.Nodes.Add(this);
            
            ProcessChildren(text,parseResult);
        }
        
        
        protected void ProcessChildren(StringReader text, ParseResult parseResult)
        {
            //There is nothing more to parse
            if (string.IsNullOrWhiteSpace(text))
            {
                if (EndNode)
                {
                    //The command string is empty and we are end node
                    parseResult.Callback = Callback;
                }
                else if (_children.Count > 0)
                {
                    //string baseExample = GetChildUsages(parseResult).FirstOrDefault();
                    
                    
                    //We didn't find a child to parse it    
                    throw new TooFewArgumentsException();
                }
                else
                {
                    //IDK... There is not more text to parse and no more children....
                    //Probably dead end.
                }
            }
            else
            {
                //Find the first child that can parse it
                var found = false;
                foreach (var child in _children)
                {
                    if (child.CanParse(text))
                    {
                        child.Parse(text, parseResult);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    string baseExample = GetUsages(parseResult).FirstOrDefault();
                    
                    
                    //We didn't find a child to parse it    
                    throw new Exception(baseExample);
                }
                
            }
            
            
        }

        public IEnumerable<string> GetUsages(ParseResult parseResult)
        {
            string baseExample = "";
            foreach (var node in parseResult.Nodes)
            {
                baseExample += (baseExample.Length>0 ? " " : "") + node.GetUsageText();
            }

            List<string> usage = new();
            foreach (var child in _children)
            {
                usage.AddRange(child.GetChildUsages(baseExample));
            }
            
            return usage;
        }
        
        public IEnumerable<string> GetChildUsages(string b)
        {
            b += (b.Length>0 ? " " : "") + GetUsageText();

            List<string> usage = new();
            if (_children.Count == 0)
            {
                usage.Add(b);
            }
            foreach (var child in _children)
            {
                usage.AddRange(child.GetChildUsages(b));
            }
            
            
            return usage;
        }

        public virtual string GetUsageText()
        {
            return "";
        }

        [Obsolete("Use "+nameof(Children)+" instead")]
        public IEnumerable<CommandNode> GetChildNodes()
        {
            return _children;
        }
    }
}