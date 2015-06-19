namespace TestConsole.DAL
{
    using System.Collections.Generic;
    using System.Linq;

    public class Fruit
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Weight { get; set; }

        public static IQueryable<Fruit> Query { get { return GetSampleFruits().AsQueryable(); } }

        private static IEnumerable<Fruit> GetSampleFruits()
        {
            yield return new Fruit { Id = 1, Name = "Banana", Weight = 42.5m };
            yield return new Fruit { Id = 2, Name = "Jagoda", Weight = 0.95m };
            yield return new Fruit { Id = 3, Name = "Šljiva", Weight = 3.3m };
            yield return new Fruit { Id = 4, Name = "Trešnja", Weight = 8.52m };
            yield return new Fruit { Id = 5, Name = "Jabuka", Weight = 22.1m };
            yield return new Fruit { Id = 6, Name = "Kivi", Weight = 15m };
        }
    }
}