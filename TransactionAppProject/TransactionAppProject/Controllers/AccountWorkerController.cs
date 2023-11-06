using Microsoft.AspNetCore.Mvc;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
using TransactionAppProject.Services;

[Route("/api/[controller]")]
[ApiController]
public class AccountWorkerController : Controller
{
    private readonly IAccountWorkerService _accountWorker;

    public AccountWorkerController(IAccountWorkerService accountWorker)
    {
        _accountWorker = accountWorker;
    }

    [HttpPost("/{indexName}/post-account-list")]
    public IActionResult PostAccountList([FromBody] IEnumerable<Account> newData, CancellationToken cancellationToken,
        string indexName)
    {
        var response = _accountWorker.PostAccountList(indexName, newData);
        if (response)
        {
            return Ok();
        }

        return StatusCode(400);

    }

    [HttpGet("/{indexName}/get-all-nodes/")]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllNodes(string indexName,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _accountWorker.GetAllAccounts(indexName);
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    [HttpGet("/{indexName}/get-node-details/")]
    public async Task<ActionResult<Account>> GetNodeDetaile([FromQuery] string accountID, string indexName, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _accountWorker.GetAccountDetails(indexName, accountID);
            if (response == null)
            {
                return BadRequest("This AccountId is not valid!");
                
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }


}