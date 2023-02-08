using Api.Data;
using Api.Models;
namespace Api.Services;

public class SubscriptionService : ISubscriptionService

{
    private ApiDbContext context;
    public SubscriptionService(ApiDbContext context) { this.context = context; }

    async public Task AddSubscriber(Subscriber subscriber)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(subscriber);
            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}