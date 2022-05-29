using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators
{
    public class PaymentValidatorFactory : IPaymentValidatorFactory
    {
        public IPaymentValidator GetInstance(MakePaymentRequest request) =>
            request.PaymentScheme switch
            {
                PaymentScheme.Bacs => new BacsValidator(),
                PaymentScheme.Chaps => new ChapsValidator(),
                PaymentScheme.FasterPayments => new FasterPaymentsValidator(),
                _ => throw new PaymentSchemeNotFoundException($"The payment scheme '{request.PaymentScheme}' was not found.")
            };
    }
}
