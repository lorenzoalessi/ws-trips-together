using WsTripsTogether.Dto.User;

namespace WsTripsTogether.Services.User;

public interface IUserService
{
    Task<UserDto> AddAsync(UserDto userDto);
    Task<string> LoginAsync(LoginDto loginDto);
}