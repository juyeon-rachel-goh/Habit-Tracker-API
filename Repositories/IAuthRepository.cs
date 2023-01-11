namespace Api.Repositories;

public interface IAuthRepository
{
    public Task<bool> DuplicateEmail(String email);
    public Task<bool> DuplicateUsername(String username);

}