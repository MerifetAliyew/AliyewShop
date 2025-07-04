using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.Validations.FavouriteValidators;
using AliyewShop.Persistence.Repositories;
using AliyewShop.Persistence.Services;
using AliyewShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using AliyewShop.Infrastructure;

namespace AliyewShop.Persistence;

public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services)
    {
        #region Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFavouriteRepository , FavouriteRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        #endregion

        #region Servicies
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICategoryService, CategoryService>();
        #endregion


    }
}
