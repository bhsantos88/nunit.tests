using Loans.Domain.Applications;
using NUnit.Framework;
using System.Collections.Generic;

namespace Loans.Tests
{
    [ProductComparison]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        // This function will run one time before the first test.
        [OneTimeSetUp] 
        public void OneTimeSetup()
        {
            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }

        // This function will run one time after the last test.
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        // This function will run before each test.
        [SetUp]
        public void Setup()
        {
            // The underline on the number is just a separator, without behaviour in the value.
            sut = new ProductComparer(new LoanAmount("US", 200_000m), products);
        }

        // This function will run after each test.
        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicatedComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // You need to know the exact value to run this test.
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnowExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            
            // Best way to do, because don't have the possibility to make mistakes with the property name (Type safe).
            //Assert.That(comparisons, Has.Exactly(1).
            //    Matches<MonthlyRepaymentComparison>(
            //    item => item.ProductName == "a" &&
            //    item.InterestRate == 1 &&
            //    item.MonthlyRepayment > 0));
            
            Assert.That(comparisons, Has.Exactly(1).
                Matches(new MonthlyRepaymentGreaterThanZeroConstraints("a", 1)));
        }
    }
}
