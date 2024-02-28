using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My_Azure_Functions_App.AzFunctions;

public class HttpFunction1
{
    private readonly ILogger<HttpFunction1> _logger;

    public HttpFunction1(ILogger<HttpFunction1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public IActionResult RunFunction1([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function RunFunction1 processed a request.");
        string[] res = new string[] { "Hello", "World", "Somewhere", "Just data", "Bye" };
        return new OkObjectResult(res);
    }

    [Function("Function2")]
    public async Task<IActionResult> Function2([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        // currently it is not automatically binding properties to model with Isolated    ([FromBody], [FromHeader], etc)
        _logger.LogInformation("C# HTTP trigger function Function2 processed a request.");
        try
        {
            var something = await req.ReadFromJsonAsync<List<string>>();
            if (something is null) return new BadRequestObjectResult("Invalid input");
            return new OkObjectResult(something);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult("Invalid input");
        }
    }


    [Function("Function400")]   public IActionResult RunFunction400([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)      { return new BadRequestObjectResult("Invalid input"); }    // Error 400
    [Function("Function401")]   public IActionResult RunFunction401([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)      { return new UnauthorizedObjectResult("Unauthorized"); }    // Error 401
    [Function("Function404")]   public IActionResult RunFunction404([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)      { return new NotFoundObjectResult("Not found"); }           // Error 404 
    [Function("Function409")]   public IActionResult RunFunction409([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)      { return new ConflictObjectResult("Conflict input"); }      // Error 409 - will produce a Conflict (409) response.
    [Function("Function420")]   public IActionResult RunFunction420([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)      { return new UnprocessableEntityObjectResult("Conflict input"); }      // Error 420 - Unprocessable

}
