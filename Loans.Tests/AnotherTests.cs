using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    public class AnotherTests
    {
        [Test]
        public void StringTests()
        {
            string name = "Bruno";

            Assert.That(name, Is.Empty); // FAIL
            Assert.That(name, Is.Not.Empty); // PASS

            Assert.That(name, Is.EqualTo("Bruno")); // PASS
            Assert.That(name, Is.EqualTo("BRUNO")); // FAIL
            Assert.That(name, Is.EqualTo("BRUNO").IgnoreCase); // PASS

            Assert.That(name, Does.StartWith("Br")); // PASS
            Assert.That(name, Does.EndWith("no")); // PASS
            Assert.That(name, Does.Contain("ru")); // PASS
            Assert.That(name, Does.Not.Contain("XYz")); // PASS

            Assert.That(name, Does.StartWith("Br")
                .And.EndWith("no")); // PASS
            Assert.That(name, Does.StartWith("xyz")
                .Or.EndWith("no")); // PASS
        }

        [Test]
        public void BooleanTests()
        {
            bool isNew = true;

            Assert.That(isNew); // PASS
            Assert.That(isNew, Is.True); // PASS

            bool areMarried = false;
            Assert.That(areMarried == false); // PASS
            Assert.That(areMarried, Is.False); // PASS
            Assert.That(areMarried, Is.Not.True); // PASS
        }

        [Test]
        public void RangeTests()
        {
            int i = 42;

            Assert.That(i, Is.GreaterThan(42)); // FAIL
            Assert.That(i, Is.GreaterThanOrEqualTo(42)); // PASS
            Assert.That(i, Is.LessThan(42)); // FAIL
            Assert.That(i, Is.LessThanOrEqualTo(42)); // PASS
            Assert.That(i, Is.InRange(40, 50)); // PASS

            DateTime d1 = new DateTime(2000, 2, 20);
            DateTime d2 = new DateTime(2000, 2, 25);

            Assert.That(d1, Is.EqualTo(d2)); // FAIL
            Assert.That(d1, Is.EqualTo(d2).Within(4).Days); // FAIL
            Assert.That(d1, Is.EqualTo(d2).Within(5).Days); // PASS
        }
    }
}
