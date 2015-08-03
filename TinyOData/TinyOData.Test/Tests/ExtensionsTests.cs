namespace TinyOData.Test.Tests
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTests : TestBase
    {
        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerCorrect()
        {
            // Arrange
            const string toTrim = "   This string    has   a      lot of   spaces  !  ";

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual("This string has a lot of spaces !", trimmed);
        }

        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerWithInnerString()
        {
            // Arrange
            const string toTrim = "   Spaces  inside   the ticks  must   'remain   the      same'   !  ";

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual("Spaces inside the ticks must 'remain   the      same' !", trimmed);
        }

        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerEmpty()
        {
            // Arrange
            string toTrim = string.Empty;

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual(string.Empty, trimmed);
        }

        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerNull()
        {
            // Arrange
            const string toTrim = null;

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.IsNull(trimmed);
        }

        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerOnlySpaces()
        {
            // Arrange
            const string toTrim = "        ";

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual(string.Empty, trimmed);
        }

        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerSingleChar()
        {
            // Arrange
            const string toTrim = "Ž";

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual("Ž", trimmed);
        }
    }
}