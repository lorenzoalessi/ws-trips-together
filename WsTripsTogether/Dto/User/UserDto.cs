namespace WsTripsTogether.Dto.User;

public class UserDto(int id, string firstName, string lastName, string username, string password, string email)
{
    public int Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Email { get; set; } = email;
}