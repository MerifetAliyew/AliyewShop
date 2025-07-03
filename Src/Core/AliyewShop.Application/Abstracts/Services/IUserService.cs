using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IUserService
{
   Task<BaseResponse<string>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
}
