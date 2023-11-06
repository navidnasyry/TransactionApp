using Microsoft.AspNetCore.Mvc;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Classes;
using TransactionAppProject.Models;

namespace TransactionAppProject.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class GraphController : Controller
{
    private readonly IGraphService _graphService;

    public GraphController(IGraphService graphService)
    {
        _graphService = graphService;
    }

    [HttpGet("/get-graph/")]
    public async Task<ActionResult<IEnumerable<Graph>>> GetGraph([FromQuery] string nodeIndex, [FromQuery] string linkIndex)
    {
        try
        {
            var response = await _graphService.CreateGraph(nodeIndex, linkIndex);
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest("You Send Bad Request...:)");
        }
        
    }
    
    [HttpGet("/node-expand/{accountId}/")]
    public async Task<ActionResult<IEnumerable<Graph>>> ExpandAccount([FromQuery] string nodeIndex, [FromQuery] string linkIndex, string accountId)
    {
        try
        {
            var response = await _graphService.ExpandNode(accountId, nodeIndex, linkIndex);
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


}