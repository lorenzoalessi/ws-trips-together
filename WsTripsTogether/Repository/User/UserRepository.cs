using Microsoft.EntityFrameworkCore;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Repository.User;

using Model;

public class UserRepository(Context context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByUsernamePasswordAsync(string username, string password) =>
        await Context.Users
            .Where(x => x.Username == username || x.Email == username) // username or email
            .Where(x => x.Password == password.ConvertToSha512())
            .FirstOrDefaultAsync();
}