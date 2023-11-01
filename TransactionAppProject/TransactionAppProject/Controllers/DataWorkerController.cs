using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
namespace TransactionAppProject.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class DataWorkerController : Controller
{

    private readonly IDataWorkerService _dataService;

    public DataWorkerController(IDataWorkerService dataService)
    {
        _dataService = dataService;
    }

    [HttpPost("/{indexName}/upload-data")]
    public IActionResult UploadData([FromBody] IEnumerable<Transaction> newData, CancellationToken cancellationToken, string indexName)
    {
        var returnVal = _dataService.PostTransactionDataList(indexName, newData);
        if (returnVal)
        {
            return Ok();
        }
        return StatusCode(400);
        
    }

    [HttpGet("/{indexName}/all-links")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllLinks(string indexName, CancellationToken cancellationToken)
    {
        try
        {
            var resp = await _dataService.AllLinks(indexName);
            return  Ok(resp);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest();
        }
    }


}