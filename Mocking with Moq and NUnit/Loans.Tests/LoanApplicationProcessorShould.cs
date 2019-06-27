using Loans.Domain.Applications;
using Moq;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            LoanApplication application = new LoanApplication(42,
                product,
                amount,
                "Bruno",
                31,
                "Mullerstr. 3, Germany", 64_999);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.False);
        }


        delegate void ValidateCallback(string applicationName,
                                       int applicantAge,
                                       string applicantAddress,
                                       ref IdentityVerificationStatus status);

        [Test]
        public void Accept()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            LoanApplication application = new LoanApplication(42,
                                                              product,
                                                              amount,
                                                              "Bruno",
                                                              31,
                                                              "Mullerstr. 3, Germany", 65_000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();

            // Setup mock with paremeters and returns.
            //mockIdentityVerifier.Setup(x => x.Validate("Bruno", 31, "Mullerstr. 3, Germany")).Returns(true);

            // Setup mock with out parameter.
            //bool isValidOutValue = true;
            //mockIdentityVerifier.Setup(x => x.Validate("Bruno", 
            //                                            31, 
            //                                            "Mullerstr. 3, Germany", 
            //                                            out isValidOutValue));
            
            // Setup mock with a ref parameter.
            mockIdentityVerifier.Setup(x => x.Validate("Bruno",
                                                        31,
                                                        "Mullerstr. 3, Germany",
                                                        ref It.Ref<IdentityVerificationStatus>.IsAny))
                                                        .Callback(new ValidateCallback(
                                                            (string applicantName,
                                                            int applicantAge,
                                                            string applicantAddress,
                                                            ref IdentityVerificationStatus status) =>
                                                            status = new IdentityVerificationStatus(true)));

            //// Using the class 'It', you can specify a specific condition to match a field.
            //// In the case below, the method will pass the tests independent of the typed values on fields.
            //// That's recommend to be used only when the value of the parameter is non-deterministic
            //mockIdentityVerifier.Setup(x => x.Validate(
            //                             It.IsAny<string>(),
            //                             It.IsAny<int>(),
            //                             It.IsAny<string>()))
            //                             .Returns(true);

            var mockCreditScorer = new Mock<ICreditScorer>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.True);
        }

        [Test]
        public void NullReturnExample()
        {
            var mock = new Mock<INullExample>();

            // Moq methods that return reference types, will return by default null.
            mock.Setup(x => x.SomeMethod());
                //.Returns<string>(null);

            string mockReturnValue = mock.Object.SomeMethod();

            Assert.That(mockReturnValue, Is.Null);
        }
    }


    public interface INullExample
    {
        string SomeMethod();
    }
}
