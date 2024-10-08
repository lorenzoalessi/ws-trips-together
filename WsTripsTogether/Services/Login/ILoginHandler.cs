using WsTripsTogether.Dto.User;

namespace WsTripsTogether.Services.Login;

public interface ILoginHandler
{
    List<UserLogged> Users { get; }
}