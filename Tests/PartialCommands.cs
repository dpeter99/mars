using System.Linq;
using System.Runtime.InteropServices;
using Mars;
using Mars.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PartialCommands
    {
        private CommandTree tree;

        [TestInitialize]
        public void Setup()
        {
            tree = new();
        }


        [TestMethod]
        public void ParseCommand_NoArgument_Single()
        {
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new LongStringArgument());

                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous");

            Assert.IsNotNull(res.error);
            Assert.IsInstanceOfType(res.error, typeof(Mars.Exceptions.TooFewArgumentsException));
        }

        [TestMethod]
        public void ParseCommand_NoArgument_Multiple()
        {
            {
                var quote = new LiteralNode("quote");
                var add = quote.NextLiteral("add");

                var ano = add.NextLiteral("anonymous");
                ano.NextArgument("text", new StringArgument())
                    .NextArgument("user", new LongStringArgument());


                tree.AddNode(quote);
            }


            var res = tree.ParseString("quote add anonymous");

            Assert.IsNotNull(res.error);
            Assert.IsInstanceOfType(res.error, typeof(Mars.Exceptions.TooFewArgumentsException));
        }


        [TestMethod]
        public void GetUsageString()
        {
            var quote = new LiteralNode("quote");
            var add = quote.NextLiteral("add");

            var ano = add.NextLiteral("anonymous");
            ano.NextArgument("text", new LongStringArgument());

            tree.AddNode(quote);


            var res = tree.ParseString("quote add anonymous");

            Assert.IsNotNull(res.error);
            Assert.IsInstanceOfType(res.error, typeof(Mars.Exceptions.TooFewArgumentsException));
            
            var usages = res.Nodes.Last().GetUsages(res).ToList();
            
            Assert.AreEqual(usages[0],"quote add anonymous <text>");
        }
    }
}