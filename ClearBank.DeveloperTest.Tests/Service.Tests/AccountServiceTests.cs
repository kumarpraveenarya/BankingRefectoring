using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Service.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountDataStoreFactory> _accountDataStoreFactory;
        private Mock<IAccountDataStore> _accountDataStore;
        private AccountService _accountService;

        [SetUp]
        public void Init()
        {
            _accountDataStoreFactory = new Mock<IAccountDataStoreFactory>();
            _accountDataStore = new Mock<IAccountDataStore>();
        }

        [Test]
        public void GetAccount_Should_Return_Valid_Response()
        {
            var account = new Account { AccountNumber = "123", Balance = 20 };
            _accountDataStore.Setup(store => store.GetAccount(It.IsAny<string>())).Returns(account);
            _accountDataStoreFactory.Setup(factory => factory.GetInstance()).Returns(_accountDataStore.Object);
            _accountService = new AccountService(_accountDataStoreFactory.Object);

            var result = _accountService.GetAccount("123");

            Assert.AreEqual(result.AccountNumber, account.AccountNumber);
        }

        [Test]
        public void UpdateAccount_Should_Return_Valid_Response()
        {
            _accountDataStoreFactory.Setup(factory => factory.GetInstance()).Returns(_accountDataStore.Object);
            _accountService = new AccountService(_accountDataStoreFactory.Object);

            _accountService.UpdateAccount(new Account { AccountNumber = "123", Balance = 30 }, new MakePaymentRequest { Amount = 20 });

            _accountDataStore.Verify(store => store.UpdateAccount(It.Is<Account>(account => account.Balance == 10)), Times.Once);
        }

    }
}
