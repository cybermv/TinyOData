﻿namespace TinyOData.Test.Tests
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Query;
    using Query.Filter.Tokens;
    using System;
    using System.Linq.Expressions;

    [TestClass]
    public class GeneralTests : TestBase
    {
        [TestMethod]
        public void Test1()
        {
            //const string filterStr = "$filter=Num eq   42   and   ( Name eq 'Jaja Bembo   ' or IsGood or Weight gt 12.5   )  or startswith  ( Name,  ' Ivo'  )";
            const string filterStr = "Num eq 42 and(Name gt 'Jaja eq (mio) bla      Bembo' or IsGood or Weight gt 12.5)or startswith(Name,'Ivo')";
            //ODataFilterQuery<SomeEntity> filter = new ODataFilterQuery<SomeEntity>(filterStr);

            TokenCollection tokenCollection = Tokenizer.Tokenize(filterStr, typeof (SomeEntity));

            Assert.AreEqual(22, tokenCollection.TokenCount);
        }

        [TestMethod]
        public void Test2()
        {
            ConstantExpression five = Expression.Constant(5);
            ConstantExpression eight = Expression.Constant(8);
            ConstantExpression two = Expression.Constant(2);

            BinaryExpression addition = Expression.Add(five, eight);
            BinaryExpression multiplication = Expression.Multiply(addition, two);

            LambdaExpression lambda = Expression.Lambda(multiplication);

            Delegate execute = lambda.Compile();

            object result = execute.DynamicInvoke();
        }
    }

    public class SomeEntity
    {
        public int Num { get; set; }

        public string Name { get; set; }

        public bool IsGood { get; set; }

        public decimal Weight { get; set; }
    }
}