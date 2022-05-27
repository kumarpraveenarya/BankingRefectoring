using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators
{
    public class DefaultValidator : IPaymentValidator
    {
        public bool IsValid(MakePaymentRequest request, Account account)
        {
            return false;
        }
    }
}
