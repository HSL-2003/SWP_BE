using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using SWP391_BE.Service;
using SWP391_BE.Middleware;
using SWP391_BE.Repo;

public void ConfigureServices(IServiceCollection services)
{
    // Add services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ISkintypeService, SkintypeService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IPaymentService, PaymentService>();
    services.AddScoped<IPromotionService, PromotionService>();
    services.AddScoped<IReviewService, ReviewService>();
    services.AddScoped<ISkinTypeService, SkinTypeService>();

    // Add repositories
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ISkinTypeRepository, SkinTypeRepository>();
    services.AddScoped<ISkinRoutineRepository, SkinRoutineRepository>();

    // Add JWT Authentication
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            // Configure JWT options
        });

    // Add AutoMapper
    services.AddAutoMapper(typeof(Startup));
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    // Other middleware configurations
} 