namespace TinyOData.Test.Tests
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTest : TestBase
    {
        [TestMethod, TestCategory("Extensions / TrimInner")]
        public void TrimInnerCorrect()
        {
            // Arrange
            const string toTrim = "   Ovo je    neki string      sa    previše    razmaka      !";

            // Act
            string trimmed = toTrim.TrimInner();

            // Assert
            Assert.AreEqual("Ovo je neki string sa previše razmaka !", trimmed);
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