namespace TinyOData.Test.Tests
{
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class ODataSkipQueryTests : TestBase
    {
        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalCorrect()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalWithSpaces()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?  $skip   =   5  ");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalNotNumber()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=NAN");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalNegativeNumber()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=-5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalHigherThanCount()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=55555");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(0, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalMistyped()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skeep=5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFCorrect()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFWithSpaces()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id &   $skip   =   5  ");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFNotNumber()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=NAN");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFNegativeNumber()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=-5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFHigherThanCount()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=55555");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(0, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFMistyped()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skeep=5");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }
    }
}