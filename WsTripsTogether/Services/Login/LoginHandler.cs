using WsTripsTogether.Dto.User;

namespace WsTripsTogether.Services.Login;

public class LoginHandler : ILoginHandler
{
    public List<UserLogged> Users { get; } = [];
}