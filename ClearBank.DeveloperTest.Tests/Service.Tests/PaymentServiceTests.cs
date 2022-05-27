using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using ClearBank.DeveloperTest.Validators.Interfaces;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Service.Tests
{
    public class PaymentServiceTests
    {
        private Mock<IPaymentValidatorFactory> _paymentValidatorFactory;
        private Mock<IPaymentValidator> _paymentValidator;
        private Mock<IAccountService> _accountService;
        private PaymentService _paymentService;

        [SetUp]
        public void Init()
        {
            _accountService = new Mock<IAccountService>();
            _paymentValidator = new Mock<IPaymentValidator>();
            _paymentValidatorFactory = new Mock<IPaymentValidatorFactory>();

            _paymentService = new PaymentService(_accountService.Object, _paymentValidatorFactory.Object);
        }

        [Test]
        public void MakePayment_Should_Return_Response_False()
        {

            _paymentValidator
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(false);

            _paymentValidatorFactory.Setup(factory => factory.GetInstance(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidator.Object);


            var result = _paymentService.MakePayment(new MakePaymentRequest());


            Assert.IsFalse(result.Success);

            _accountService.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Never);
        }

        [Test]
        public void MakePayment_Should_Return_Response_True()
        {

            _paymentValidator
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(true);

            _paymentValidatorFactory.Setup(factory => factory.GetInstance(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidator.Object);


            var result = _paymentService.MakePayment(new MakePaymentRequest());


            Assert.IsTrue(result.Success);

            _accountService.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);

        }
    }
}
