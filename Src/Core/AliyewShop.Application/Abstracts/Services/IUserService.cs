using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IUserService
{
   Task<BaseResponse<string>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
    Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<BaseResponse<string>> AddRole(UserAddRoleDto dto);
    Task<BaseResponse<string>> ConfirmEmail(string userId, string token);
    Task<BaseResponse<string>> ResetPasswordAsync(UserResetPasswordDto dto);
    Task<BaseResponse<string>> SendResetPasswordEmailAsync(string email);
    Task<BaseResponse<List<UserGetDto>>> GetAllUsersAsync();
    Task<BaseResponse<UserGetDto>> GetUserByIdAsync(string id);
    Task<BaseResponse<UserProfileDto>> GetMyProfileAsync(string userId);
}
