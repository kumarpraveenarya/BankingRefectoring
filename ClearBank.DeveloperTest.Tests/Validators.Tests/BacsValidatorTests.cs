using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators.Tests
{
    [TestFixture]
    public class BacsValidatorTests
    {
        private BacsValidator _bacsValidator;

        [SetUp]
        public void Init()
        {
            _bacsValidator = new BacsValidator();
        }

        [Test]
        public void IsValid_Should_Return_True_When_Account_Is_Allowed_In_Payment_Scheme_Bacs()
        {

            var result = _bacsValidator.IsValid(new MakePaymentRequest(), new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs });


            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Not_Allowed_In_Payment_Scheme_Bacs()
        {

            var result = _bacsValidator.IsValid(new MakePaymentRequest(), new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments });

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Null()
        {

            var result = _bacsValidator.IsValid(new MakePaymentRequest(), null);


            Assert.IsFalse(result);
        }
    }
}