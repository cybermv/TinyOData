namespace TinyOData.Test.DAL
{
    using System.Data.Entity;

    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext()
            : base("name=TestDatabase")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}