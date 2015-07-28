namespace TinyOData.Test.Tests
{
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class ODataOrderByQueryTests : TestBase
    {
        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleCorrect()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(7, finalList[6].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleAscending()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id asc");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(7, finalList[6].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleDescending()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id desc");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(12, finalList[10].Id);
            Assert.AreEqual(13, finalList[9].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleWithSpaces()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?   $orderby =   Id    desc");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(12, finalList[10].Id);
            Assert.AreEqual(13, finalList[9].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleNonExistingProperty()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=BadPropertyName");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleBadDirection()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id what");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleCorrect()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Color, Price desc");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual("Apple", finalList[0].Name);
            Assert.AreEqual(228m, finalList[10].Price);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleWithSpaces()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby  =  Color   ,  Price    desc  ");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual("Apple", finalList[0].Name);
            Assert.AreEqual(228m, finalList[10].Price);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleNonExistingProperty()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Color, Price desc, IDontExist asc");

            // Act
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }
    }
}