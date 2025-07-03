using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Application.Shared.Settings;
using AliyewShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AliyewShop.Persistence.Services;

public class UserService : IUserService
{

    private UserManager<AppUser> _userManager { get; }
    private SignInManager<AppUser> _signInManager { get; }
    private  JWTSettings _jwtSetting { get; }
    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTSettings> jWTSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSetting = jWTSettings.Value;
    }
    public async Task<BaseResponse<string>> Register(UserRegisterDto dto)
    {
        // 1. Email ilə istifadəçi artıq varsa, xətanı qaytar
        var existedEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is not null)
        {
            return new BaseResponse<string>("Bu email ilə artıq istifadəçi mövcuddur", HttpStatusCode.BadRequest);
        }

        // 2. Yeni istifadəçini yarat
        var newUser = new AppUser
        {
            Fullname = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };

        // 3. Şifrə ilə yarat
        var identityResult = await _userManager.CreateAsync(newUser, dto.Password);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors.Select(e => e.Description);
            return new BaseResponse<string>(string.Join("; ", errors), HttpStatusCode.BadRequest);
        }

        // 4. Enumdan string-ə çevirib rolu əlavə et
        var roleName = dto.Role.ToString(); // Buyer və ya Seller
        var roleResult = await _userManager.AddToRoleAsync(newUser, roleName);

        if (!roleResult.Succeeded)
        {
            var errors = roleResult.Errors.Select(e => e.Description);
            return new BaseResponse<string>(string.Join("; ", errors), HttpStatusCode.BadRequest);
        }

        // 5. Uğurlu nəticə
        return new BaseResponse<string>("İstifadəçi uğurla yaradıldı", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto)
    {

        var existedEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is null)
        {
            return new("Email or password os wrong.", HttpStatusCode.NotFound);
        }


        SignInResult signInResult = await _signInManager.PasswordSignInAsync
            (dto.Email, dto.Password, true, true);
        if (!signInResult.Succeeded)
        {
            return new("Email or password os wrong.", null, HttpStatusCode.NotFound);
        }
        var token = GenerateJwtToken(dto.Email);
        var expires = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes);
        TokenResponse tokenResponse = new()
        {
            Token = token,
            ExpireDate = expires
        };
        return new("Token generated",tokenResponse, HttpStatusCode.OK);
    }

    public string GenerateJwtToken(string userEmail)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.Email, userEmail),
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
