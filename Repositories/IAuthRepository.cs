namespace Api.Repositories;

public interface IAuthRepository
{
    public Task<bool> DuplicateEmail(String email);

}