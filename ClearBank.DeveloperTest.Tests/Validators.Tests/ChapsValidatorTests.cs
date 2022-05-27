using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators.Tests
{
    [TestFixture]
    public class ChapsValidatorTests
    {
        private ChapsValidator _chapsValidator;

        [SetUp]
        public void Init()
        {
            _chapsValidator = new ChapsValidator();
        }

        [Test]
        public void IsValid_Should_Return_True_When_Account_Is_In_State_Live()
        {

            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Live
                });


            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Not_Allowed_In_Payment_Scheme_Chaps()
        {

            var result = _chapsValidator.IsValid(
                 new MakePaymentRequest(),
                 new Account
                 {
                     AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                     Status = AccountStatus.Live
                 });


            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_In_State_Disabled()
        {

            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Disabled
                });


            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_In_State_Live()
        {

            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.InboundPaymentsOnly
                });


            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Null()
        {

            var result = _chapsValidator.IsValid(new MakePaymentRequest(), null);

            Assert.IsFalse(result);
        }
    }
}