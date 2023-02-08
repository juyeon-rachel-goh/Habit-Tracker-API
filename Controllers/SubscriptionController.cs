using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("api/subscription")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService subscriptionService;
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        this.subscriptionService = subscriptionService;
    }

    [HttpPost]
    [Route("")]
    async public Task<ActionResult<Subscriber>> AddDailyRecord([FromBody] Subscriber value)
    {
        if (value == null)
        {
            return BadRequest();
        }

        value.Id = Guid.NewGuid();
        await this.subscriptionService.AddSubscriber(value);
        return Ok();
        //need error handling
    }


}
