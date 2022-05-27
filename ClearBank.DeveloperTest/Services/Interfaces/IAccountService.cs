using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces
{
    public interface IAccountService
    {
        Account GetAccount(string accountNumber);

        void UpdateAccount(Account account, MakePaymentRequest request);

    }
}
