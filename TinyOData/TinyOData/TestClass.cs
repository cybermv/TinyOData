using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyOData
{
    using System.Linq.Expressions;

    internal class TestClass
    {
        private void f()
        {
            //ConstantExpression numThree = Expression.Constant(5m);

            //ParameterExpression param = Expression.Parameter(typeof(Fruit), "fruit");
            //MemberExpression fruitWeight = Expression.Property(param, "Weight");

            //BinaryExpression isFruitWeightGreaterThanNumThree = Expression.GreaterThan(fruitWeight, numThree);

            //LambdaExpression lambdaForHeavyFruits = Expression.Lambda(isFruitWeightGreaterThanNumThree, param);

            //MethodCallExpression filterFruits = Expression.Call(
            //    typeof(Queryable),
            //    "Where",
            //    new Type[] { param.Type },
            //    Fruit.Query.Expression,
            //    lambdaForHeavyFruits);

            //IQueryable queryable = Fruit.Query.Provider.CreateQuery(filterFruits);

            //IQueryable<Fruit> fruitsFiltered = queryable as IQueryable<Fruit>;

            //Fruit[] array = fruitsFiltered.ToArray();

            //IQueryable<Fruit> appliedQuery = query.ApplyTo(Fruit.Query);
        }
    }
}