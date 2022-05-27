using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Interfaces
{
    public interface IPaymentValidator
    {
        bool IsValid(MakePaymentRequest request, Account account);
    }
}
