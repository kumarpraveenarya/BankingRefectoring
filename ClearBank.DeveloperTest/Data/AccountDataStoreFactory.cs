using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services.Interfaces;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStoreFactory : IAccountDataStoreFactory
    {
        private readonly IConfigSettings _configSettings;

        public AccountDataStoreFactory(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public IAccountDataStore GetInstance()
        {
            if (_configSettings.DataStoreType == Constants.DataStoreTypeConstant.Backup)
                return new BackupAccountDataStore();

            return new AccountDataStore();
        }
    }
}
