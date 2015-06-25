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
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(7, finalList[6].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleAscending()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id asc");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(7, finalList[6].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleDescending()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id desc");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(12, finalList[10].Id);
            Assert.AreEqual(13, finalList[9].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleWithSpaces()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?   $orderby =   Id    desc");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(12, finalList[10].Id);
            Assert.AreEqual(13, finalList[9].Id);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleNonExistingProperty()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=BadPropertyName");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }

        [TestMethod, TestCategory("Queries / OrderBy / Single")]
        public void OrderBySingleBadDirection()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id what");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleCorrect()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Color, Price desc");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual("Apple", finalList[0].Name);
            Assert.AreEqual(228m, finalList[10].Price);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleWithSpaces()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby  =  Color   ,  Price    desc  ");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual("Apple", finalList[0].Name);
            Assert.AreEqual(228m, finalList[10].Price);
        }

        [TestMethod, TestCategory("Queries / OrderBy / Multiple")]
        public void OrderByMultipleNonExistingProperty()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Color, Price desc, IDontExist asc");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(true, allProducts.SequenceEqual(finalList));
        }
    }
}