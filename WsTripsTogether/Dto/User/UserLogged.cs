namespace WsTripsTogether.Dto.User;

public class UserLogged(UserDto user, string token)
{
    public UserDto User { get; set; } = user;

    public string Token { get; set; } = token;
}