namespace Mars
{
    public static class CommandBuildHelper
    {
        public static void Next(this CommandNode c, CommandNode n)
        {
            c.AddChild(n);
        }
        
        public static LiteralNode NextLiteral(this CommandNode c, string txt)
        {
            var node = new LiteralNode(txt);
            c.AddChild(node);

            return node;
        }
        
        public static ArgumentNode<T> NextArgument<T>(this CommandNode c, string txt, ArgumentType<T> type, CommandMetadata meta = null)
        {
            var node = new ArgumentNode<T>(txt,meta ,type);
            c.AddChild(node);

            return node;
        }

        public static CommandNode ThisCalls(this CommandNode c, CommandCallback callback)
        {
            c.Callback = callback;
            c.EndNode = true;

            return c;
        }
         
    }
}