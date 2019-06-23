using NUnit.Framework;
using System.Collections;

namespace Loans.Tests
{
    public class MonthlyRepaymentTestData
    {
        // This static data will centralize test cases to avoid repeating the same data in different tests.
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(200_000m, 6.5m, 30, 1264.14m);
                yield return new TestCaseData(500_000m, 10m, 30, 4387.86m);
            }
        }
    }
}
