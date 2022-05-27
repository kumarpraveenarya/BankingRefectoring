using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators
{
    public class BacsValidator : IPaymentValidator
    {
        public bool IsValid(MakePaymentRequest request, Account account)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
}
