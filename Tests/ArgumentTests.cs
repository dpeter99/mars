using System;
using Mars;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ArgumentTests
    {
        
        [TestMethod]
        public void ParseStringArgument()
        {
            var arg = new Mars.Arguments.StringArgument();

            var text = new StringReader( "token1 token2");
            var res = arg.Parse(text,null);

            Assert.AreEqual(text, "token2");
            Assert.AreEqual(res, "token1");
        }
        
        [TestMethod]
        public void ParseStringArgument_last()
        {
            var arg = new Mars.Arguments.StringArgument();

            var text = new StringReader( "token1");
            var res = arg.Parse(text,null);

            Assert.AreEqual(text, "");
            Assert.AreEqual(res, "token1");
        }
        
        
        [TestMethod]
        public void ParseLongStringArgument()
        {
            var arg = new Mars.Arguments.LongStringArgument();

            var text = new StringReader( "token1 token2");
            var res = arg.Parse(text,null);

            Assert.AreEqual(text, "");
            Assert.AreEqual(res, "token1 token2");
        }
        
        [TestMethod]
        public void ParseIntArgument()
        {
            var arg = new Mars.Arguments.IntArgument();

            var text = new StringReader("123 token2");
            var res = arg.Parse(text,null);

            Assert.AreEqual(text, "token2");
            Assert.AreEqual(res, 123);
        }

        [TestMethod]
        public void ParseIntArgument_Last()
        {
            var arg = new Mars.Arguments.IntArgument();

            var text = new StringReader("123");
            var res = arg.Parse(text,null);

            Assert.AreEqual(text, "");
            Assert.AreEqual(res, 123);
        }
        
        [TestMethod]
        public void ParseIntArgument_Bad()
        {
            var arg = new Mars.Arguments.IntArgument();

            var text = new StringReader("1s23");



            Assert.ThrowsException<ArgumentException>(() =>
            {
                var res = arg.Parse(text, null);
            });
            
            Assert.AreEqual(text, "1s23");
        }
        
    }
}