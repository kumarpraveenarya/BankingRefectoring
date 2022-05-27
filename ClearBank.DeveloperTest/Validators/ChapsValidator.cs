using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators
{
    public class ChapsValidator : IPaymentValidator
    {
        public bool IsValid(MakePaymentRequest request, Account account)
        {
            return account != null &&
                  account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
                  account.Status == AccountStatus.Live;
        }
    }
}
