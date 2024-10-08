using Microsoft.EntityFrameworkCore;
using WsTripsTogether.Model;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Repository.User;

public class UserRepository(Context context) : GenericRepository<Model.User>(context), IUserRepository
{
    public async Task<Model.User?> GetByUsernamePasswordAsync(string username, string password) =>
        await Context.Users
            .Where(x => x.Username == username || x.Email == username) // username or email
            .Where(x => x.Password == password.ConvertToSha512())
            .FirstOrDefaultAsync();
}