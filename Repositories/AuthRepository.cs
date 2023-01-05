using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class AuthRepository : IAuthRepository
{
    private ApiDbContext context;
    public AuthRepository(ApiDbContext context)
    {
        this.context = context;
    }

    async public Task<bool> DuplicateEmail(String email)
    {
        var hasDuplicateEmail = false;
        var duplicateEmailCount = await this.context.Users
         .Where(
           dbUsers => dbUsers.Email == email
         ).CountAsync();

        if (duplicateEmailCount >= 1)
        {
            hasDuplicateEmail = true;

        }
        return hasDuplicateEmail;
    }
}
