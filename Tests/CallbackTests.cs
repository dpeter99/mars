using Mars;
using Mars.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CallbackTests
    {
        private CommandTree tree;
        
        [TestInitialize]
        public void Setup()
        {
            tree = new();
        }
        
        [TestMethod]
        public void GetCallback_Literal()
        {
            var callback = new CommandCallbackSimple("TEST");
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                
                var ano = add.NextLiteral("anonymous").ThisCalls( callback);

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous");

            Assert.AreEqual(res.Callback, callback);
        }
        
        [TestMethod]
        public void GetCallback_String()
        {
            var callback = new CommandCallbackSimple("TEST");
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");


                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new StringArgument()).ThisCalls( callback);

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous test_text");

            Assert.AreEqual(res.Callback, callback);
            
            Assert.AreEqual(res.Args["text"].ToString(), "test_text");
            Assert.AreEqual(res.Args.Count, 1);
        }
        
        [TestMethod]
        public void GetCallback_LongString()
        {
            var callback = new CommandCallbackSimple("TEST");
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");


                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new LongStringArgument()).ThisCalls(callback);

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous test_text but way longer now");

            Assert.AreEqual(res.Callback, callback);
            
            Assert.AreEqual(res.Args["text"].ToString(), "test_text but way longer now");
            Assert.AreEqual(res.Args.Count, 1);
        }
        
        [TestMethod]
        public void GetCallback_Int()
        {
            var callback = new CommandCallbackSimple("TEST");
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");


                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new IntArgument()).ThisCalls(callback);

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous 123456789");

            Assert.AreEqual(res.Callback, callback);
            
            Assert.AreEqual(res.Args["text"].ToString(), "123456789");
            Assert.AreEqual(res.Args["text"], 123456789);
            Assert.AreEqual(res.Args.Count, 1);
        }

        
    }
}