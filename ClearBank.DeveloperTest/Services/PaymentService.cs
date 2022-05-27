using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentValidatorFactory _paymentValidatorFactory;

        public PaymentService(IAccountService accountService, IPaymentValidatorFactory paymentValidatorFactory)
        {
            _accountService = accountService;
            _paymentValidatorFactory = paymentValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);

            var paymentValidator = _paymentValidatorFactory.GetInstance(request);

            var isValid = paymentValidator.IsValid(request, account);


            if (!isValid)
            {
                return new MakePaymentResult { Success = false };
            }

            _accountService.UpdateAccount(account, request);

            return new MakePaymentResult { Success = true };
        }       

    }
}
