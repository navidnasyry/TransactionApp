using Moq;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
using TransactionAppProject.Services;

namespace TransactionAppProject.Test.Services.Test;

public class AccountWorkerServiceTest
{
    private readonly string _indexName = "newIndexName";

    private readonly Account _account = new()
    {
        AccountID = "1",
        CardID = "123339933",
        Shaba = "IR456789876",
        AccountType = "chom",
        BranchTelephone = "000999999",
        BranchAdress = "addr",
        BranchName = "choom",
        OwnerName = "name",
        OwnerFamilyName = "skdjf",
        OwnerID = "993993"
    };
    private readonly List<Account> _dataSample = new()
    {
        new()
        {
            AccountID = "1",
            CardID = "123339933",
            Shaba = "IR456789876",
            AccountType = "chom",
            BranchTelephone = "000999999",
            BranchAdress = "addr",
            BranchName = "choom",
            OwnerName = "name",
            OwnerFamilyName = "skdjf",
            OwnerID = "993993"
        },
        new()
        {
            AccountID = "2",
            CardID = "67765677",
            Shaba = "086545678",
            AccountType = "chom",
            BranchTelephone = "3333333",
            BranchAdress = "add",
            BranchName = "choom",
            OwnerName = "name",
            OwnerFamilyName = "gfff",
            OwnerID = "99393"
        }
    };

    [Fact]
    public void PostAccountList_WithoutException_ReturnFalse()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        clientRepo.Setup(x => x.IndexManyData(_dataSample, _indexName)).Returns(false);
        var accountWorker = new AccountWorkerService(clientRepo.Object);

        // Act
        var returnVal = accountWorker.PostAccountList(_indexName, _dataSample);

        // Assert
        Assert.False(returnVal);
    }
    [Fact]
    public void PostAccountList_WithException_ReturnFalse()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        clientRepo.Setup(x => x.IndexManyData(_dataSample, _indexName)).Throws(new Exception());
        var accountWorker = new AccountWorkerService(clientRepo.Object);

        // Act
        var returnVal = accountWorker.PostAccountList(_indexName, _dataSample);

        // Assert
        Assert.False(returnVal);

    }
    [Fact]
    public void PostAccountList_ReturnTrue()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        clientRepo.Setup(x => x.IndexManyData(_dataSample, _indexName)).Returns(true);
        var accountWorker = new AccountWorkerService(clientRepo.Object);

        // Act
        var returnVal = accountWorker.PostAccountList(_indexName, _dataSample);

        // Assert
        Assert.True(returnVal);
    }

    [Fact]
    public void GetAllAccounts_ThrowException()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        clientRepo.Setup(x => x.SearchAsyncAllData(_indexName)).Throws(new GetDataFromElasticException());
        var accountWorker = new AccountWorkerService(clientRepo.Object);
        
        // Act
        var resultMethod = accountWorker.GetAllAccounts;

        // Assert
        Assert.ThrowsAsync<GetDataFromElasticException>(async () => await resultMethod(_indexName));
    }

    [Fact]
    public async Task GetAllAccount_ReturnData()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        clientRepo.Setup(x => x.SearchAsyncAllData(_indexName)).Returns(Task.FromResult(_dataSample.AsEnumerable()));
        var accountWorker = new AccountWorkerService(clientRepo.Object);
        
        // Act
        var resultMethod = await accountWorker.GetAllAccounts(_indexName);

        // Assert
        Assert.Equivalent(_dataSample, resultMethod);
    }

    [Fact]
    public async Task GetAccountDetails_ReturnData()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        var accountId = _account.AccountID;
        clientRepo.Setup(x => x.GetOneNodeDetailes(accountId, _indexName)).Returns(Task.FromResult(_account));
        var accountWorker = new AccountWorkerService(clientRepo.Object);
        // Act

        var returnVal = await accountWorker.GetAccountDetails(_indexName, accountId);
        
        // Assert
        Assert.Equivalent(_account, returnVal);


    }
    [Fact]
    public void GetAccountDetails_ThrowException()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        var accountId = _account.AccountID;
        clientRepo.Setup(x => x.GetOneNodeDetailes(accountId, _indexName)).Throws(new GetDataFromElasticException());
        var accountWorker = new AccountWorkerService(clientRepo.Object);
        
        // Act
        var returnObj = accountWorker.GetAccountDetails;
        
        // Assert
        Assert.ThrowsAsync<GetDataFromElasticException>(async () => await returnObj(accountId, _indexName));
        
    }
    
    [Fact]
    public async Task GetAccountDetails_ReturnNull_AccountIdNotExist()
    {
        // Arrange
        var clientRepo = new Mock<IElasticClientRepository<Account>>();
        var accountId = "1232";
        clientRepo.Setup(x => x.GetOneNodeDetailes(accountId, _indexName)).Returns(Task.FromResult((Account)null));
        var accountWorker = new AccountWorkerService(clientRepo.Object);
        
        // Act
        var returnVal = await accountWorker.GetAccountDetails(_indexName, accountId);
        
        // Assert
        Assert.Null(returnVal);

    }
    
}