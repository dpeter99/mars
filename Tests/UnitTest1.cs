using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mars;
using Mars.Arguments;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private CommandTree tree; 
        
        [TestInitialize]
        public void Setup()
        {
            tree = new();

            // var setting = new LiteralNode("setting");
            // setting.NextLiteral("on/off_switch").Callback = new CommandCallbackSimple("switch");
            //
            // setting.NextLiteral("intsetting").NextArgument("int", new IntArgument());
            //
            // tree.AddNode(setting);
        }
        
        
        [TestMethod]
        public void ParseCommand_WithLongString()
        {
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new LongStringArgument());

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous long string with many spaces in it");

            Assert.AreEqual(res.Args["text"].ToString(), "long string with many spaces in it");
            Assert.AreEqual(res.Args.Count, 1);
        }
        
        [TestMethod]
        public void ParseCommand_WithStringAndLongString()
        {
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                var from = add.NextLiteral("from");
                from.NextArgument("source", new StringArgument())
                .NextArgument("text", new LongStringArgument());
            
                tree.AddNode(quote);
            }
            
            var res = tree.ParseString("quote add from asd long string with many spaces in it");

            Assert.AreEqual(res.Args["source"].ToString(), "asd");
            Assert.AreEqual(res.Args["text"].ToString(), "long string with many spaces in it");
            Assert.AreEqual(res.Args.Count, 2);
        }
        
        [TestMethod]
        public void ParseCommand_WithString()
        {
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                var from = add.NextLiteral("from");
                from.NextArgument("source", new StringArgument());
            
                tree.AddNode(quote);
            }
            
            var res = tree.ParseString("quote add from asd");

            Assert.AreEqual(res.Args["source"].ToString(), "asd");
            Assert.AreEqual(res.Args.Count, 1);
        }
        
        [TestMethod]
        public void ParseCommand_LiteralOnly()
        {
            {
                var setting = new LiteralNode("setting");
                setting.NextLiteral("on/off_switch").ThisCalls(new CommandCallbackSimple("switch"));

                tree.AddNode(setting);
            }
            
            var res = tree.ParseString("setting on/off_switch");

            Assert.AreEqual(((CommandCallbackSimple)res.Callback).name, "switch");
        }
        
        [TestMethod]
        public void ParseCommand_IntOnly()
        {
            {
                var setting = new LiteralNode("setting");

                setting.NextLiteral("intsetting").NextArgument("int", new IntArgument());
            
                tree.AddNode(setting);
            }
            
            var res = tree.ParseString("setting intsetting 1234");

            Assert.AreEqual(res.Args["int"], 1234);
            Assert.AreEqual(res.Args.Count, 1);
        }
        
        [TestMethod]
        public void ParseCommand_GetCallback()
        {
            {
                //tree = new();
                
                var setting = new LiteralNode("setting");
                setting.NextLiteral("on/off_switch").ThisCalls(new CommandCallbackSimple("switch"));

                setting.NextLiteral("intsetting").NextArgument("int", new IntArgument()).ThisCalls( new CommandCallbackSimple("arg"));
            
                tree.AddNode(setting);
            }
            
            var res = tree.ParseString("setting on/off_switch");

            Assert.AreEqual(((CommandCallbackSimple)res.Callback).name, "switch");
            
            res = tree.ParseString("setting intsetting 123");
            
            Assert.AreEqual(((CommandCallbackSimple)res.Callback).name, "arg");
        }
        
        [TestMethod]
        public void ParseCommand_GetCallbackAndString()
        {
            {
                //tree = new();
                
                var great = new Mars.LiteralNode("greet");
                great.NextArgument("user", new StringArgument()).ThisCalls(new CommandCallbackSimple("hi"));
            
                tree.AddNode(great);
            }

            var res = tree.ParseString("greet me");
            
            Assert.AreEqual(((CommandCallbackSimple)res.Callback).name, "hi");
            
            Assert.AreEqual(res.Args["user"].ToString(), "me");
            Assert.AreEqual(res.Args.Count, 1);
        }
    }
}