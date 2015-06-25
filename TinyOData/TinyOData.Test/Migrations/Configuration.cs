namespace TinyOData.Test.Migrations
{
    using DAL;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductsDbContext context)
        {
            context.Products.AddOrUpdate(
                p => p.Name,
                Product.GetSampleProducts().ToArray());
        }
    }
}