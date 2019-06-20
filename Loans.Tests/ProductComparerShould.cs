using Loans.Domain.Applications;
using NUnit.Framework;
using System.Collections.Generic;

namespace Loans.Tests
{

    public class ProductComparerShould
    {
        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            // The underline on the number is just a separator, without behaviour in the value.
            var sut = new ProductComparer(new LoanAmount("US", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicatedComparisons()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                //new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            // The underline on the number is just a separator, without behaviour in the value.
            var sut = new ProductComparer(new LoanAmount("US", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            // The underline on the number is just a separator, without behaviour in the value.
            var sut = new ProductComparer(new LoanAmount("US", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // You need to know the exact value to run this test.
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnowExpectedValues()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            // When you don't know about the exact values, you can specify the fields that you know.
            var sut = new ProductComparer(new LoanAmount("US", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            
            //Assert.That(comparisons, Has.Exactly(1)
            //    .Property("ProductName").EqualTo("a")
            //    .And
            //    .Property("InterestRate").EqualTo(1)
            //    .And
            //    .Property("MonthlyRepayment").GreaterThan(0));

            // Best way to do, because don't have the possibility to make mistakes with the property name (Type safe).
            Assert.That(comparisons, Has.Exactly(1).
                Matches<MonthlyRepaymentComparison>(
                item => item.ProductName == "a" &&
                item.InterestRate == 1 &&
                item.MonthlyRepayment > 0));
        }
    }
}
