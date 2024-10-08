namespace WsTripsTogether.Repository.User;

using Model;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernamePasswordAsync(string username, string password);
}