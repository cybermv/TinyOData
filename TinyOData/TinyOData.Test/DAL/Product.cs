namespace TinyOData.Test.DAL
{
    using System.Collections.Generic;

    public class Product
    {
        public Product(string name, string color, int rating, decimal price, bool isAvailable)
            : this(name, color, rating, price, isAvailable, null, null)
        {
        }

        public Product(string name, string color, int rating, decimal price, bool available, int? amountProduced, int? amountSelled)
        {
            Name = name;
            Color = color;
            Rating = rating;
            Price = price;
            Available = available;
            AmountProduced = amountProduced;
            AmountSelled = amountSelled;
        }

        public Product()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public int Rating { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public int? AmountProduced { get; set; }

        public int? AmountSelled { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1} = {2}, {3}",
                Name,
                Color,
                Price.ToString("C"),
                Available ? "available" : "not available");
        }

        public static IEnumerable<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product("Car", "Red", 3, 1586.68m, true, 1500, 1200),
                new Product("Tent", "Green", 2, 228m, true, 8000, 1500),
                new Product("House", "White", 4, 353.99m, false, 10500, 18800),
                new Product("Truck", "Blue", 4, 47.5m, true),
                new Product("Banana", "Yellow", 1, 299.67m, true),
                new Product("Mustard", "Black", 1, 78m, false, 90, 50),
                new Product("Bathtub", "Red", 5, 8987.08m, false, 120, 80),
                new Product("Oil filter", "Yellow", 5, 150m, true, 1400, 5000),
                new Product("Chair", "Red", 3, 4007m, true),
                new Product("Table", "Green", 4, 578.67m, true),
                new Product("Sofa", "Blue", 2, 999.98m, false, 1500, 39),
                new Product("Staircase", "Yellow", 2, 78m, false, 8600, 1200),
                new Product("Shoe box", "White", 1, 8m, true),
                new Product("Drawing kit", "Red", 5, 1.8m, false, 799, 455),
                new Product("Ceiling fan", "Yellow", 3, 16.22m, true, 90, 650),
                new Product("First aid kit", "Green", 4, 897.8m, false, 1120, 2500),
                new Product("Apple", "Black", 3, 1234.56m, true, 7007, 0),
                new Product("Movie", "Blue", 4, 1.08m, true, 12, 0),
                new Product("Monitor", "Red", 2, 558m, true, 554, 60),
                new Product("Radio", "Green", 1, 624.9m, false),
                new Product("Clock", "Blue", 1, 9000.09m, false),
                new Product("Chocolate", "Black", 5, 12.34m, true, 1480, 50000)
            };
        }
    }
}