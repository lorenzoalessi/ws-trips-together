using AutoMapper;
using WsTripsTogether.Dto.User;
using WsTripsTogether.Exceptions;
using WsTripsTogether.Repository.User;

namespace WsTripsTogether.Services.User;

using Model;

public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
{
    public async Task<UserDto> AddAsync(UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        await userRepository.AddAsync(user);
        
        // Update dto ID with no other mapping
        userDto.Id = user.Id;
        return userDto;
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await userRepository.GetByUsernamePasswordAsync(loginDto.Username, loginDto.Password);
        if (user == null)
            throw new UserException("User not found");

        // TODO: token generation
        
        return string.Empty;
    }
}