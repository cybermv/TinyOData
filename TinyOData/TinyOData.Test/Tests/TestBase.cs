namespace TinyOData.Test.Tests
{
    using DAL;
    using Extensions;
    using Query;
    using System;
    using System.Linq;

    /// <summary>
    /// Base class for all tests
    /// </summary>
    public class TestBase : IDisposable
    {
        private ProductsDbContext _context;

        /// <summary>
        /// Gets a base <see cref="IQueryable{Product}"/> instance on which
        /// to apply test queries; is't underlying store is a DbSet
        /// </summary>
        /// <returns>An <see cref="IQueryable{Product}"/> instance</returns>
        protected IQueryable<Product> GetDatabaseQueryable()
        {
            if (this._context == null)
            {
                this._context = new ProductsDbContext();
            }
            return this._context.Products;
        }

        /// <summary>
        /// Gets a base <see cref="IQueryable{Product}"/> instance on which
        /// to apply test queries; it's underlying store is a list
        /// </summary>
        /// <returns>An <see cref="IQueryable{Product}"/> instance</returns>
        protected IQueryable<Product> GetLocalQueryable()
        {
            return Product.GetSampleProducts().AsQueryable();
        }

        /// <summary>
        /// Creates a sample query by using a dummy base url and appending
        /// the given string to the url
        /// </summary>
        /// <param name="queryStringPart">The query part of the url</param>
        /// <returns>An <see cref="ODataQuery{Product}"/> instance</returns>
        protected ODataQuery<Product> CreateQuery(string queryStringPart)
        {
            Uri uri = new Uri("http://mydomain.com/api/products" + queryStringPart);
            QueryString queryString = uri.ParseODataQueryString();
            return new ODataQuery<Product>(queryString);
        }

        /// <summary>
        /// Disposes the <see cref="ProductsDbContext"/> instance
        /// </summary>
        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
            }
        }
    }
}