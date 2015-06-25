namespace TinyOData.Test.DAL
{
    using System.Data.Entity;

    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}