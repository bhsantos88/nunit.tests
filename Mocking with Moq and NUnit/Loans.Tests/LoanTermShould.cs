using System;
using System.Collections.Generic;
using Loans.Domain;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    // Tests will be separeted by Arrange, Act and Assert phases.
    // Arrange: set up test object(s), initialize test data, etc.
    // Act: call method, set property, etc.
    // Assert: compare returned value/end state with expected.
    // * That is a pattern, but you may not have three explicit phases.

    // About the quality of the tests.
    // Fast: needs to be run as quickly as possible.
    // Repeatable: the result needs to be the same in the same circustances. 
    // Isolated: If one test require another tests, it's not isolated, therefore it's not a good test. 
    // Trustworthy: The results of the tests must not show doubts.
    // Valuable: The result of the tests needs to be valuable.For example, make tests for Auto Properties in C# (the compiler will make this behind the scenes) don't add any value
    //to the software.

    public class MyObject : ValueObject
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public MyObject(int value, string name)
        {
            Value = value;
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }



    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        [Ignore("Need to complete update work.")]
        public void ReturnTermInMonths()
        {
            // sut means 'SYSTEM UNDER TEST'
            var sut = new LoanTerm(1);

            string customErrorMsg = "[ReturnTermInMonths] Months should be 12 * numbers of years";
            Assert.That(sut.ToMonths(), Is.EqualTo(12), customErrorMsg);
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            MyObject a = new MyObject(1, "Bruno");
            MyObject b = a;
            MyObject c = new MyObject(1, "Bruno");

            Assert.That(a, Is.SameAs(b));
            //Assert.That(a, Is.EqualTo(c)); // TEST WILL FAILS.
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;

            //Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            //Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
            //    .With
            //    .Message
            //    .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(
                ex => ex.ParamName == "years"));
        }
    }
}
