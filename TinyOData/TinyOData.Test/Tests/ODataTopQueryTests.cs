namespace TinyOData.Test.Tests
{
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class ODataTopQueryTests : TestBase
    {
        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryCorrect()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$top=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryWithSpaces()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("? $top =   5 ");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryNotNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$top=blabla");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryNegativeNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$top=-42");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryHigherThanCount()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$top=1000000");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Top")]
        public void TopQueryMistyped()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$tops=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }
    }
}