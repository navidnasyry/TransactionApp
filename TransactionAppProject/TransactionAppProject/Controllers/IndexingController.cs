using Microsoft.AspNetCore.Mvc;
using Nest;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
using TransactionAppProject.Services;

namespace TransactionAppProject.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class IndexingController : Controller
{
    private readonly IIndexingService _indexService;
    
    public IndexingController(IIndexingService indexService)
    {
        _indexService = indexService;
    }
    
    [HttpPost("/create-index")]
    public async Task<IActionResult> CreateIndex([FromBody] ElasticIndex newIndex, CancellationToken cancellationToken)
    {
        try
        {
            var returnValue = _indexService.CreateIndex(newIndex.Name);
            if (returnValue)
            {
                return Ok("Index created Successfuly");
            }
            else
            {
                return StatusCode(500);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return StatusCode(500);
        }
    }

    
}