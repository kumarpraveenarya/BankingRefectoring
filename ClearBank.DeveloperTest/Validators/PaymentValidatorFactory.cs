using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators
{
    public class PaymentValidatorFactory : IPaymentValidatorFactory
    {
        public IPaymentValidator GetInstance(MakePaymentRequest request) => request.PaymentScheme switch
        {
            PaymentScheme.Bacs => new BacsValidator(),
            PaymentScheme.FasterPayments => new FasterPaymentsValidator(),
            PaymentScheme.Chaps => new ChapsValidator(),
            _ => new DefaultValidator()
        };
    }
}
