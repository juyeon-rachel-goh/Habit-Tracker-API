using Api.Models;

namespace Api.Services;


public interface ISubscriptionService

{
    public Task AddSubscriber(Subscriber subscriber);
}