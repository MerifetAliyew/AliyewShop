using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Application.Shared.Settings;
using AliyewShop.Domain.Entities;
using AliyewShop.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AliyewShop.Persistence.Services;

public class UserService : IUserService
{

    private UserManager<AppUser> _userManager { get; }
    private IEmailService _mailService { get; }
    private SignInManager<AppUser> _signInManager { get; }
    private  JWTSettings _jwtSetting { get; }
    private RoleManager<IdentityRole> _roleManager { get; }
    public UserService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JWTSettings> jWTSettings,
        RoleManager<IdentityRole> roleManger,
        IEmailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSetting = jWTSettings.Value;
        _roleManager = roleManger;
        _mailService = mailService;
    }
    public async Task<BaseResponse<string>> Register(UserRegisterDto dto)
    {
        // Email artıq varsa, qeydiyyata icazə verilmir
        var existedEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is not null)
        {
            return new BaseResponse<string>("This account already exists", HttpStatusCode.BadRequest);
        }

        // Rol adını enumdan string kimi al (Buyer/Seller)
        var roleName = dto.Role.ToString();

        // Rol mövcuddursa davam et, yoxdursa error qaytar
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            return new BaseResponse<string>("Selected role is invalid", HttpStatusCode.BadRequest);
        }

        // Yeni istifadəçini yarat
        var newUser = new AppUser
        {
            Fullname = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };

        // İstifadəçini yaradıb şifrəni təyin et
        var identityResult = await _userManager.CreateAsync(newUser, dto.Password);
        if (!identityResult.Succeeded)
        {
            var errorsMessage = string.Join(";", identityResult.Errors.Select(e => e.Description));
            return new(errorsMessage, HttpStatusCode.BadRequest);
        }

        // İstifadəçiyə rol ver
        var roleResult = await _userManager.AddToRoleAsync(newUser, roleName);
        if (!roleResult.Succeeded)
        {
            var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
            return new($"Failed to assign role: {errors}", HttpStatusCode.BadRequest);
        }

        // Email təsdiqləmə linki yaradılır
        string confirmEmailLink = await GetEmailConfirmLink(newUser);
        await _mailService.SendEmailAsync(
            new List<string> { newUser.Email },
            "Email Confirmation",
            confirmEmailLink
        );

        // Rol ilə birlikdə mesajı geri qaytar
        var message = $"User registered successfully with role: {roleName}. Please confirm email.";
        return new BaseResponse<string>(message, confirmEmailLink, HttpStatusCode.Created);
    }

    public async Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto)
    {

        var existedEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is null)
        {
            return new("Email or password is wrong.", null, System.Net.HttpStatusCode.NotFound);
        }

        if (!existedEmail.EmailConfirmed)
        {
            return new("Please confirm your email", null, System.Net.HttpStatusCode.BadRequest);
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, true);

        if (!signInResult.Succeeded)
        {
            return new("Email or password is wrong.", null, System.Net.HttpStatusCode.NotFound);
        }

        var token = await GenerateTokensAsync(existedEmail);
        return new("Token generated", token, System.Net.HttpStatusCode.OK);
    }

    private async Task<TokenResponse> GenerateTokensAsync(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));

            // Hər rol üçün permission-ları əlavə et
            var identityRole = await _roleManager.FindByNameAsync(role);
            if (identityRole != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(identityRole);
                foreach (var claim in roleClaims.Where(c => c.Type == "Permission"))
                {
                    claims.Add(claim);
                }
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
            Issuer = _jwtSetting.Issuer,
            Audience = _jwtSetting.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiryDate = DateTime.UtcNow.AddHours(2); //  7 day  
        user.RefreshToken = refreshToken;
        user.ExpiryDate = refreshTokenExpiryDate;
        await _userManager.UpdateAsync(user);

        return new TokenResponse
        {
            Token = jwt,
            RefreshToken = refreshToken,
            ExpireDate = tokenDescriptor.Expires!.Value
        };
    }

    public async Task<BaseResponse<string>> AddRole(UserAddRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
        {
            return new BaseResponse<string>("User not found.", HttpStatusCode.NotFound);
        }

        var roleNames = new List<string>();

        foreach (var roleId in dto.RolesId.Distinct())
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return new BaseResponse<string>($"Role with ID '{roleId}' not found.", HttpStatusCode.NotFound);
            }
            if (!await _userManager.IsInRoleAsync(user, role.Name!))
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name!);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new BaseResponse<string>($"Failed to add role '{role.Name}' to user: {errors}", HttpStatusCode.BadRequest);
                }

                roleNames.Add(role.Name!);
            }
        }
        return new BaseResponse<string>(
          $"Successfully added roles: {string.Join(", ", roleNames)} to user.",
              HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> ConfirmEmail(string userId, string token)
    {
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null)
        {
            return new BaseResponse<string>("Email confirmation failed.", HttpStatusCode.NotFound);
        }

        var result = await _userManager.ConfirmEmailAsync(existedUser, token);
        if (!result.Succeeded)
        {
            return new BaseResponse<string>("Email confirmation failed.", HttpStatusCode.BadRequest);
        }

        return new BaseResponse<string>("Email confirmed successfully.", HttpStatusCode.OK);
    }

    private async Task<string> GetEmailConfirmLink(AppUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = $"https://localhost:7197/api/Accounts/confirm-email?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
        Console.WriteLine(token);
        return link;

    }

    public async Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return new("Invalid access token", null, HttpStatusCode.Unauthorized);

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null)
            return new("User not found", null, HttpStatusCode.NotFound);

        if (user.RefreshToken is null || user.RefreshToken != request.RefreshToken ||
            user.ExpiryDate < DateTime.UtcNow)
            return new("Invalid refresh token", null, HttpStatusCode.BadRequest);

        var newAccessToken = await GenerateTokensAsync(user);
        return new("Token refreshed", newAccessToken, HttpStatusCode.OK);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false, // Token-in vaxtını yoxlama
            ValidIssuer = _jwtSetting.Issuer,
            ValidAudience = _jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<BaseResponse<string>> ResetPasswordAsync(UserResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return new BaseResponse<string>("User not found", HttpStatusCode.NotFound);

        var decodedToken = WebUtility.UrlDecode(dto.Token);
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return new BaseResponse<string>(errors, HttpStatusCode.BadRequest);
        }

        return new BaseResponse<string>("Password has been reset successfully.", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> SendResetPasswordEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new BaseResponse<string>("User not found", HttpStatusCode.NotFound);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);

        var resetLink = $"https://localhost:7041/api/Accounts/reset-password?email={email}&token={encodedToken}";

        await _mailService.SendEmailAsync(
            new List<string> { email },
            "Password Reset",
            $"Please reset your password by clicking {resetLink}");

        return new BaseResponse<string>("Password reset link has been sent to your email.", HttpStatusCode.OK);
    }
}
