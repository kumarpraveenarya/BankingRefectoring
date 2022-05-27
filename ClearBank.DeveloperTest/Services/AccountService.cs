using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _accountDataStoreFactory;

        public AccountService(IAccountDataStoreFactory accountDataStoreFactory)
        {
            _accountDataStoreFactory = accountDataStoreFactory.GetInstance();
        }


        public Account GetAccount(string accountNumber)
        {
            return _accountDataStoreFactory.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account, MakePaymentRequest request)
        {
            account.Balance -= request.Amount;

            _accountDataStoreFactory.UpdateAccount(account);
        }

    }
}
