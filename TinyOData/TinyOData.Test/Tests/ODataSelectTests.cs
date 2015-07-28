namespace TinyOData.Test.Tests
{
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class ODataSelectTests : TestBase
    {
        [TestMethod, TestCategory("Queries / Select")]
        public void SelectCorrect()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$select=Name,Price,Available");

            // Act
            IQueryable<dynamic> appliedQuery = odataQuery.ApplyToAsDynamic(baseQueryable);
            List<dynamic> selection = appliedQuery.ToList();

            // Assert
            Type selectionType = selection[0].GetType();
            Assert.AreEqual(3, selectionType.GetFields().Count());
            Assert.AreEqual(typeof(string), selectionType.GetFields().Single(f => f.Name == "Name").FieldType);
            Assert.AreEqual(typeof(decimal), selectionType.GetFields().Single(f => f.Name == "Price").FieldType);
            Assert.AreEqual(typeof(bool), selectionType.GetFields().Single(f => f.Name == "Available").FieldType);
        }

        [TestMethod, TestCategory("Queries / Select")]
        public void SelectWithSpaces()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?  $select  =  Name ,    Price    ,  Available   ");

            // Act
            IQueryable<dynamic> appliedQuery = odataQuery.ApplyToAsDynamic(baseQueryable);
            List<dynamic> selection = appliedQuery.ToList();

            // Assert
            Type selectionType = selection[0].GetType();
            Assert.AreEqual(3, selectionType.GetFields().Count());
            Assert.AreEqual(typeof(string), selectionType.GetFields().Single(f => f.Name == "Name").FieldType);
            Assert.AreEqual(typeof(decimal), selectionType.GetFields().Single(f => f.Name == "Price").FieldType);
            Assert.AreEqual(typeof(bool), selectionType.GetFields().Single(f => f.Name == "Available").FieldType);
        }

        [TestMethod, TestCategory("Queries / Select")]
        public void SelectWrongCasing()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$select=name,PRICE,AvaILAble");

            // Act
            IQueryable<dynamic> appliedQuery = odataQuery.ApplyToAsDynamic(baseQueryable);
            List<dynamic> selection = appliedQuery.ToList();

            // Assert
            Type selectionType = selection[0].GetType();
            Assert.AreEqual(3, selectionType.GetFields().Count());
            Assert.AreEqual(typeof(string), selectionType.GetFields().Single(f => f.Name == "Name").FieldType);
            Assert.AreEqual(typeof(decimal), selectionType.GetFields().Single(f => f.Name == "Price").FieldType);
            Assert.AreEqual(typeof(bool), selectionType.GetFields().Single(f => f.Name == "Available").FieldType);
        }

        [TestMethod, TestCategory("Queries / Select")]
        public void SelectDuplicatedProperty()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$select=Name, Color, Name, Price, coLOR");

            // Act
            IQueryable<dynamic> appliedQuery = odataQuery.ApplyToAsDynamic(baseQueryable);
            List<dynamic> selection = appliedQuery.ToList();

            // Assert
            Type selectionType = selection[0].GetType();
            Assert.AreEqual(3, selectionType.GetFields().Count());
            Assert.AreEqual(typeof(string), selectionType.GetFields().Single(f => f.Name == "Name").FieldType);
            Assert.AreEqual(typeof(string), selectionType.GetFields().Single(f => f.Name == "Color").FieldType);
            Assert.AreEqual(typeof(decimal), selectionType.GetFields().Single(f => f.Name == "Price").FieldType);
        }

        [TestMethod, TestCategory("Queries / Select")]
        public void SelectNonExistingProperty()
        {
            // Arrange
            IQueryable<Product> baseQueryable = GetDatabaseQueryable();
            ODataQuery<Product> odataQuery = CreateQuery("?$select=Name,Surname,Available");

            // Act
            IQueryable<dynamic> appliedQuery = odataQuery.ApplyToAsDynamic(baseQueryable);
            List<dynamic> selection = appliedQuery.ToList();

            // Assert
            Type selectionType = selection[0].GetType();
            Assert.AreEqual(typeof(Product), selectionType);
        }
    }
}