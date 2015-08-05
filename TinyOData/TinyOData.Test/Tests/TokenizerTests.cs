namespace TinyOData.Test.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query.Filter.Tokens;

    [TestClass]
    public class TokenizerTests : TestBase
    {
        [TestMethod, TestCategory("Tokenizer")]
        public void SimpleQuery()
        {
            // Arrange
            const string query = "Id gt 5";

            // Act
            TokenCollection tokenCollection = Tokenizer.Tokenize<TestEntity>(query);

            // Assert
            Assert.AreEqual(3, tokenCollection.TokenCount);
            Assert.AreEqual(TokenKind.PropertyAccessor, tokenCollection[0].Kind);
            Assert.AreEqual("Id", tokenCollection[0].Value);
            Assert.AreEqual(TokenKind.ComparisonOperator, tokenCollection[1].Kind);
            Assert.AreEqual("gt", tokenCollection[1].Value);
            Assert.AreEqual(TokenKind.LiteralValue, tokenCollection[2].Kind);
            Assert.AreEqual("5", tokenCollection[2].Value);
        }

        [TestMethod, TestCategory("Tokenizer")]
        public void SimpleQueryWithSpaces()
        {
            // Arrange
            const string query = "  Id    gt     5  ";

            // Act
            TokenCollection tokenCollection = Tokenizer.Tokenize<TestEntity>(query);

            // Assert
            Assert.AreEqual(3, tokenCollection.TokenCount);
            Assert.AreEqual(TokenKind.PropertyAccessor, tokenCollection[0].Kind);
            Assert.AreEqual("Id", tokenCollection[0].Value);
            Assert.AreEqual(TokenKind.ComparisonOperator, tokenCollection[1].Kind);
            Assert.AreEqual("gt", tokenCollection[1].Value);
            Assert.AreEqual(TokenKind.LiteralValue, tokenCollection[2].Kind);
            Assert.AreEqual("5", tokenCollection[2].Value);
        }

        [TestMethod, TestCategory("Tokenizer")]
        public void QueryWithLogicOperators()
        {
            // Arrange
            const string query = "Id gt 5 or IsTrue or Name eq 'Ivo' and Weight le 87.8";

            // Act
            TokenCollection tokenCollection = Tokenizer.Tokenize<TestEntity>(query);

            // Assert
            Assert.AreEqual(13, tokenCollection.TokenCount);
            Assert.AreEqual(TokenKind.LogicOperator, tokenCollection[3].Kind);
            Assert.AreEqual("or", tokenCollection[3].Value);
            Assert.AreEqual(TokenKind.PropertyAccessor, tokenCollection[4].Kind);
            Assert.AreEqual("IsTrue", tokenCollection[4].Value);
            Assert.AreEqual(TokenKind.LiteralValue, tokenCollection[8].Kind);
            Assert.AreEqual("'Ivo'", tokenCollection[8].Value);
            Assert.AreEqual(TokenKind.LiteralValue, tokenCollection[12].Kind);
            Assert.AreEqual("87.8", tokenCollection[12].Value);
        }

        [TestMethod, TestCategory("Tokenizer")]
        public void QueryWithLogicOperatorsNegation()
        {
            // Arrange
            const string query = "Id gt 5 or not IsTrue and Id le 42";

            // Act
            TokenCollection tokenCollection = Tokenizer.Tokenize<TestEntity>(query);

            // Assert
            Assert.AreEqual(10, tokenCollection.TokenCount);
            Assert.AreEqual(TokenKind.LogicOperator, tokenCollection[3].Kind);
            Assert.AreEqual("or", tokenCollection[3].Value);
            Assert.AreEqual(TokenKind.LogicOperator, tokenCollection[4].Kind);
            Assert.AreEqual("not", tokenCollection[4].Value);
            Assert.AreEqual(TokenKind.LogicOperator, tokenCollection[6].Kind);
            Assert.AreEqual("and", tokenCollection[6].Value);
        }


        [TestMethod, TestCategory("Tokenizer")]
        public void QueryWithFunctionCalls()
        {
            // Arrange
            const string query = "startswith(substring(Name, 2), 'teo') and length(Name) eq 5";

            // Act
            TokenCollection tokenCollection = Tokenizer.Tokenize<TestEntity>(query);

            // Assert
            Assert.AreEqual(18, tokenCollection.TokenCount);
            Assert.AreEqual(TokenKind.FunctionCall, tokenCollection[0].Kind);
            Assert.AreEqual("startswith", tokenCollection[0].Value);
            Assert.AreEqual(TokenKind.LeftParenthesis, tokenCollection[1].Kind);
            Assert.AreEqual(TokenKind.FunctionCall, tokenCollection[2].Kind);
            Assert.AreEqual("substring", tokenCollection[2].Value);
            Assert.AreEqual(TokenKind.FunctionCall, tokenCollection[12].Kind);
            Assert.AreEqual("length", tokenCollection[12].Value);
            Assert.AreEqual(TokenKind.PropertyAccessor, tokenCollection[14].Kind);
            Assert.AreEqual("Name", tokenCollection[14].Value);
        }
        
        internal class TestEntity
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Weight { get; set; }

            public bool IsTrue { get; set; }
        }
    }
}
