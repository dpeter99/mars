using Mars;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class StringReaderTests
    {


        [TestMethod]
        public void NextToken()
        {
            StringReader reader = new StringReader("token1 token2 token3");
            
            Assert.AreEqual(reader.GetNextToken(),"token1");
            Assert.AreEqual(reader.GetText(),"token1 token2 token3");
        }
        
        [TestMethod]
        public void PopNextToken()
        {
            StringReader reader = new StringReader("token1 token2 token3");

            var t = reader.PopNextToken();
            Assert.AreEqual(t,"token1");
            Assert.AreEqual(reader.GetText(),"token2 token3");
            Assert.AreEqual(reader.GetFullText(),"token1 token2 token3");
        }
        
        [TestMethod]
        public void PopNextToken_Multiple()
        {
            StringReader reader = new StringReader("token1 token2 token3");

            
            
            Assert.AreEqual(reader.PopNextToken(),"token1");
            Assert.AreEqual(reader.GetText(),"token2 token3");
            
            
            Assert.AreEqual(reader.PopNextToken(),"token2");
            Assert.AreEqual(reader.GetText(),"token3");
            
            Assert.AreEqual(reader.PopNextToken(),"token3");
            Assert.AreEqual(reader.GetText(),"");
            
            Assert.AreEqual(reader.GetFullText(),"token1 token2 token3");
        }
        
        [TestMethod]
        public void PopNextToken_Last()
        {
            StringReader reader = new StringReader("token1");

            var t = reader.PopNextToken();
            Assert.AreEqual(t,"token1");
            Assert.AreEqual(reader.GetText(),"");
            Assert.AreEqual(reader.GetFullText(),"token1");
        }
        
        [TestMethod]
        public void PopNextToken_Empty()
        {
            StringReader reader = new StringReader("");

            var t = reader.PopNextToken();
            Assert.AreEqual(t,"");
            Assert.AreEqual(reader.GetText(),"");
            Assert.AreEqual(reader.GetFullText(),"");
        }
        
        [TestMethod]
        public void ConvertToString()
        {
            StringReader reader = new StringReader("token1 token2 token3");
            
            Assert.AreEqual(reader,"token1 token2 token3");
        }
        
        [TestMethod]
        public void ConvertToString_shorter()
        {
            StringReader reader = new StringReader("token1 token2 token3");
            reader.PopNextToken();
            
            Assert.AreEqual(reader,"token2 token3");
        }
        
        [TestMethod]
        public void TestToString()
        {
            StringReader reader = new StringReader("token1 token2 token3");
            
            Assert.AreEqual(reader.ToString(),"token1 token2 token3");
        }
    }
}