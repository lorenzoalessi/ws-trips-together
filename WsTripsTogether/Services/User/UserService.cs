using AutoMapper;
using WsTripsTogether.Dto.User;
using WsTripsTogether.Exceptions;
using WsTripsTogether.Repository.User;
using WsTripsTogether.Services.Login;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Services.User;

public class UserService(
    IMapper mapper,
    IUserRepository userRepository,
    ILoginHandler loginHandler) : IUserService
{
    public async Task<UserDto> AddAsync(UserDto userDto)
    {
        var user = mapper.Map<Model.User>(userDto);
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

        var userDto = mapper.Map<UserDto>(user);
        // Token generation
        var token = JwtUtils.GenerateJwtToken(userDto);
        
        loginHandler.Users.Add(new UserLogged(userDto, token));

        return token;
    }
}