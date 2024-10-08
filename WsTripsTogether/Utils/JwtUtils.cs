using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WsTripsTogether.Dto.User;

namespace WsTripsTogether.Utils;

public static class JwtUtils
{
    private const string Issuer = "tripstogether";

    public static string GenerateJwtToken(UserDto userDto)
    {
        // Credentials
        var credentials = GetCredentials();
        
        // Claims
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString())
        };
        
        // Token generation
        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: userDto.Username,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static JwtSecurityToken DecodeToken(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token);
    
    public static bool IsValidToken(JwtSecurityToken token, UserDto userDto)
    {
        // Issuer
        if (token.Issuer != Issuer)
            return false;
        
        // Audience
        var audiences = token.Audiences.ToArray();
        if (audiences.Length != 1 || audiences.FirstOrDefault() != userDto.Username)
            return false;

        // Claims
        var claims = token.Claims.ToArray();
        var checkClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier && x.Value == userDto.Id.ToString());
        if (checkClaim == null)
            return false;

        // Expiration (end)
        return token.ValidTo >= DateTime.Now;
    }
    
    private static SigningCredentials GetCredentials()
    {
        // Encrypt the key (randomly generated) and obtain the credentials to sign the token
        // (inline key but perhaps more correct to set it in a setting in the properties file)
        var securityKey = new SymmetricSecurityKey("zdf+1A34S1B+CJVfUIEeQ5cLkgZXFiJPDxpSPv47J2A="u8.ToArray());
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
}