using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
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
        public void MakePayment_Should_Throw_Exception_if_Account_is_Invalid()
        {
            _accountService.Setup(x => x.GetAccount("invalid account")).Returns(() => null);
            Assert.Throws<DebtorAccountNotFoundException>(() => _paymentService.MakePayment(new MakePaymentRequest()));
        }


        [Test]
        public void MakePayment_Should_Return_Response_False_if_Request_not_Valid()
        {
            _accountService.Setup(x => x.GetAccount("123")).Returns(new Account()
            {
                AccountNumber = "123"
            });

            _paymentValidator
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(false);

            _paymentValidatorFactory.Setup(factory => factory.GetInstance(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidator.Object);


            var result = _paymentService.MakePayment(new MakePaymentRequest{DebtorAccountNumber = "123"});


            Assert.IsFalse(result.Success);

            _accountService.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Never);
        }

        [Test]
        public void MakePayment_Should_Return_Response_True_If_Request_is_Valid()
        {
            _accountService.Setup(x => x.GetAccount("123")).Returns(new Account()
            {
                AccountNumber = "123"
            });

            _paymentValidator
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(true);

            _paymentValidatorFactory.Setup(factory => factory.GetInstance(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidator.Object);


            var result = _paymentService.MakePayment(new MakePaymentRequest{ DebtorAccountNumber = "123" });


            Assert.IsTrue(result.Success);

            _accountService.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);

        }

        [Test]
        public void MakePayment_Should_Throw_Exception_If_Payment_Scheme_is_not_Valid()
        {
            _accountService.Setup(x => x.GetAccount("123")).Returns(new Account()
            {
                AccountNumber = "123"
            });

           _paymentValidatorFactory.Setup(factory => factory.GetInstance(It.IsAny<MakePaymentRequest>()))
                .Returns(() => throw new PaymentSchemeNotFoundException("test"));

           Assert.Throws<PaymentSchemeNotFoundException>(() => _paymentService.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "123", PaymentScheme = (PaymentScheme)(-1)}));
        }
    }
}
