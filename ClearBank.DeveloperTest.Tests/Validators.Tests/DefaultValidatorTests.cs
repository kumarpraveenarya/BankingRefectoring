using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators.Tests
{
    [TestFixture]
    public class DefaultValidatorTests
    {

        private DefaultValidator _defaultValidator;

        [SetUp]
        public void Init()
        {
            _defaultValidator = new DefaultValidator();
        }

        [Test]
        public void IsValid_Should_Return_False()
        {

            var result = _defaultValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Live
                });


            Assert.IsFalse(result);
        }
    }
}