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
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalWithSpaces()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?  $skip   =   5  ");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalNotNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=NAN");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalNegativeNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=-5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalHigherThanCount()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skip=55555");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(0, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / Local")]
        public void SkipQueryLocalMistyped()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetLocalQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$skeep=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFCorrect()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFWithSpaces()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id &   $skip   =   5  ");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count - 5, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFNotNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=NAN");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFNegativeNumber()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=-5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFHigherThanCount()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skip=55555");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            Assert.AreEqual(0, finalList.Count);
        }

        [TestMethod, TestCategory("Queries / Skip / EF")]
        public void SkipQueryOnEFMistyped()
        {
            // Prepare
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$orderby=Id & $skeep=5");

            // Test
            IQueryable<Product> appliedQuery = odataQuery.ApplyTo(baseQueryable);
            List<Product> finalList = appliedQuery.ToList();

            // Assert
            List<Product> allProducts = baseQueryable.ToList();
            Assert.AreEqual(allProducts.Count, finalList.Count);
        }
    }
}