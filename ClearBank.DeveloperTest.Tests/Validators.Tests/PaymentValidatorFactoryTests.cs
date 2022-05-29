using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators.Tests
{
    [TestFixture]
    public class PaymentValidatorFactoryTests
    {
        private PaymentValidatorFactory _paymentValidatorFactory;

        [SetUp]
        public void Init()
        {
            _paymentValidatorFactory = new PaymentValidatorFactory();
        }

        [Test]
        public void GetInstance_Should_Return_BacsValidator()
        {
            var result = _paymentValidatorFactory.GetInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs });

            Assert.IsInstanceOf<BacsValidator>(result);
        }

        [Test]
        public void GetInstance_Should_Return_FasterPaymentsValidator()
        {
            var result = _paymentValidatorFactory.GetInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments });

            Assert.IsInstanceOf<FasterPaymentsValidator>(result);
        }

        [Test]
        public void GetInstance_Should_Return_ChapsValidator()
        {
            var result = _paymentValidatorFactory.GetInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps });

            Assert.IsInstanceOf<ChapsValidator>(result);
        }

        [Test]
        public void GetInstance_Should_ThrowsException()
        {
            Assert.Throws<PaymentSchemeNotFoundException>(() =>
                _paymentValidatorFactory.GetInstance(new MakePaymentRequest { PaymentScheme = (PaymentScheme)(-1) }));
        }

    }
}